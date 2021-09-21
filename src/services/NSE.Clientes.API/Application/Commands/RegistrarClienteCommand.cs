using NSE.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Application.Commands
{
    public class RegistrarClienteCommand : Command
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }

        public RegistrarClienteCommand(Guid id, string nome, string email, string cpf)
        {
            this.AggregateId = id;
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Cpf = cpf;
    }
    }
}
