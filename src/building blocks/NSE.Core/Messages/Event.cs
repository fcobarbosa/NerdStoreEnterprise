using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSE.Core.Messages
{
    public class Event : Message, INotification
    {
        public DateTime Timestamp { get; set; }
        public Event()
        {
            this.Timestamp = DateTime.Now;
        }
    }
}
