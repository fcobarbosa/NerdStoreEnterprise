using EasyNetQ;
using NSE.Core.Messages.Integration;
using Polly;
using RabbitMQ.Client.Exceptions;
using System;
using System.Threading.Tasks;

namespace NSE.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private IBus _bus;
        private IAdvancedBus _advancedBus;
        private readonly string _connectionString;
        public bool IsConnected => this.AdvancedBus?.IsConnected ?? false;
        public IAdvancedBus AdvancedBus => this._bus?.Advanced;
        public MessageBus(string connectionString)
        {
            this._connectionString = connectionString;
            TryConnect();
        }

        public void Publish<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            this._bus.PubSub.Publish<T>(message);
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            await this._bus.PubSub.PublishAsync(message);
        }

        public TResponse Request<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return this._bus.Rpc.Request<TRequest, TResponse>(request);
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return await this._bus.Rpc.RequestAsync<TRequest, TResponse>(request);
        }

        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> response)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return this._bus.Rpc.Respond(response);
        }

        public async Task<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> response)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return await this._bus.Rpc.RespondAsync<TRequest, Task<TResponse>>(response);
        }

        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            TryConnect();
            this._bus.PubSub.Subscribe(subscriptionId, onMessage);
        }

        public async void SubscribeAsync<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            TryConnect();
            await this._bus.PubSub.SubscribeAsync(subscriptionId, onMessage);
        }

        public void TryConnect()
        {
            if (IsConnected) return;
            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(retryCount: 3, sleepDurationProvider: retryAttempt =>
                     TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
            policy.Execute(action: 
                () => { 
                    this._bus = this._bus = RabbitHutch.CreateBus(this._connectionString);
                    this._advancedBus = this._bus.Advanced;
                    this._advancedBus.Disconnected += OnDisconnect;
                });
        }

        private void OnDisconnect(object s, EventArgs e)
        {
            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .RetryForever();

            policy.Execute(TryConnect);
        }

        public void Dispose()
        {
            this._bus.Dispose();
        }
    }

    public interface IMessageBus
    {
        IAdvancedBus AdvancedBus { get; }
        bool IsConnected { get; }
        public void Publish<T>(T message) where T : IntegrationEvent;
        Task PublishAsync<T>(T message) where T : IntegrationEvent;
        TResponse Request<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;
        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;
        IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> response)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;
        Task<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> response)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;

        void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class;
        void SubscribeAsync<T>(string subscriptionId, Action<T> onMessage) where T : class;
        void TryConnect();
    }
}
