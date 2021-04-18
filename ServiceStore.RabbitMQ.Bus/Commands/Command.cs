using ServiceStore.RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceStore.RabbitMQ.Bus.Commands
{
    public abstract class Command : Message
    {
        public DateTime TimeStamp { get; protected set; }
        protected Command()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
