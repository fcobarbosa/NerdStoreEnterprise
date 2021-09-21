using FluentValidation.Results;
using MediatR;
using NSE.Clientes.API.Models;
using NSE.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Application.Commands
{
    public class ClienteCommandHandler : CommandHandler, IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;
            var cliente = new Cliente(message.Id, message.Nome, message.Email, message.Cpf);

            //validações de negócio

            //persistir no banco
            if (true)
            {
                AddError("Esse CPF já está em uso");
                return this.ValidationResult;
            }              
            return message.ValidationResult;
        }
    }
}
