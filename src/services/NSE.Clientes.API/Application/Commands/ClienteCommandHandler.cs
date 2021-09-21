using FluentValidation.Results;
using MediatR;
using NSE.Clientes.API.Application.Events;
using NSE.Clientes.API.Models;
using NSE.Core.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Application.Commands
{
    public class ClienteCommandHandler : CommandHandler, IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteCommandHandler(IClienteRepository clienteRepository)
        {
            this._clienteRepository = clienteRepository;
        }
        public async Task<ValidationResult> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;
            var cliente = new Cliente(message.Id, message.Nome, message.Email, message.Cpf);

            var clienteExistente = await this._clienteRepository.GetByCpf(cliente.Cpf.Numero);

            if (clienteExistente == null)
            {
                AddError("Esse CPF já está em uso");
                return this.ValidationResult;
            }

            //lançar um evento cliente ok
            cliente.AddNotification(new ClienteRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));

            this._clienteRepository.Add(cliente);
            return await PersistData(this._clienteRepository.UnitOfWork);
        }
    }
}
