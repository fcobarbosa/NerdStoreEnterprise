using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
