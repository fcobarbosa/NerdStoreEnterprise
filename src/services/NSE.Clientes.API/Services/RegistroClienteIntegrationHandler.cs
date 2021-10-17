using EasyNetQ;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Services
{
    public class RegistroClienteIntegrationHandler : BackgroundService
    {
        private IBus _bus;
        private readonly IServiceProvider _serviceProvider;
        public RegistroClienteIntegrationHandler(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this._bus = RabbitHutch.CreateBus(connectionString: "host=192.168.99.100:5672");

            this._bus.Rpc.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(responder: async request =>
            new ResponseMessage(await RegistrarCliente(request)));

            return Task.CompletedTask;
        }

        public async Task<ValidationResult> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
        {
            ValidationResult success;
            var clienteCommand = new RegistrarClienteCommand(message.Id, message.Nome, message.Email, message.Cpf);
            using(var scope = this._serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                success = await mediator.SendCommand(clienteCommand);
            }
            return success;
        }
    }
}
