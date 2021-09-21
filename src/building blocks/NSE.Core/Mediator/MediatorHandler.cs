using FluentValidation.Results;
using MediatR;
using NSE.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSE.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        public MediatorHandler(IMediator mediator)
        {
            this._mediator = mediator;
        }
        public async Task PublishEvent<T>(T evento) where T : Event
        {
            await this._mediator.Publish(evento);
        }

        public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return await this._mediator.Send(command);
        }
    }
}
