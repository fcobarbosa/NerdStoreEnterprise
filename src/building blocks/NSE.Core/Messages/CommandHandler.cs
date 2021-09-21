using FluentValidation.Results;
using NSE.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSE.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;
        public CommandHandler()
        {
            this.ValidationResult = new ValidationResult();
        }
        protected void AddError(string message)
        {
            AddError(string.Empty, message);
        }

        protected void AddError(string propertyName, string message)
        {
            this.ValidationResult.Errors.Add(new ValidationFailure(propertyName, message));
        }

        protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
        {
            if (!await uow.Commit()) AddError("Houve um erro ao persistir os dados");
            return ValidationResult;
        }
    }
}
