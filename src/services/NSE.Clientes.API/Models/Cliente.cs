using NSE.Core.DomainObjects;
using System;

namespace NSE.Clientes.API.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public bool Excluido { get; private set; }
        public Endereco Endereco { get; private set; }

        protected Cliente() { }
        public Cliente(Guid id, string nome, string email, string cpf)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = new Email(email);
            this.Cpf = new Cpf(cpf);
            this.Excluido = false;
        }

        public void TrocarEmail(string email)
        {
            this.Email = new Email(email);
        }

        public void AtribuirEndereco(Endereco endereco)
        {
            this.Endereco = endereco;
        }
    }
}
