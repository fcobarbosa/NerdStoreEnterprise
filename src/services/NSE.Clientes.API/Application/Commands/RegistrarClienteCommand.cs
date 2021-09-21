using FluentValidation;
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
        public override bool IsValid()
        {
            this.ValidationResult = new RegistrarClienteValidation().Validate(this);
            return this.ValidationResult.IsValid;
        }

        public class RegistrarClienteValidation : AbstractValidator<RegistrarClienteCommand>
        {
            public RegistrarClienteValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do cliente inválido");

                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do cliente não foi informado");

                RuleFor(c => c.Cpf)
                    .Must(CpfIsValid)
                    .WithMessage("O CPF informado não é válido");

                RuleFor(c => c.Email)
                    .Must(EmailIsValid)
                    .WithMessage("O e-mail informado não é válido");
            }

            protected static bool CpfIsValid(string cpf)
            {
                return Core.DomainObjects.Cpf.Validar(cpf);
            }
            protected static bool EmailIsValid(string email)
            {
                return Core.DomainObjects.Email.Validar(email);
            }
        }
    }
}
