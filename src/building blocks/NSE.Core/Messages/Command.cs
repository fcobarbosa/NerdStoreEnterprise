﻿using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSE.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; protected set; }
        public Command()
        {
            this.Timestamp = DateTime.Now;
        }

        public virtual bool IsValid() 
        {
            throw new NotImplementedException();
        }
    }
}
