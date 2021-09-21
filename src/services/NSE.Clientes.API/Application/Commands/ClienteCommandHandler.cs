using FluentValidation.Results;
using MediatR;
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

            //validações de negócio
            var clienteExistente = await this._clienteRepository.GetByCpf(cliente.Cpf.Numero);
            //persistir no banco
            if (clienteExistente == null)
            {
                AddError("Esse CPF já está em uso");
                return this.ValidationResult;
            }
            this._clienteRepository.Add(cliente);
            return await PersistData(this._clienteRepository.UnitOfWork);
        }
    }
}
