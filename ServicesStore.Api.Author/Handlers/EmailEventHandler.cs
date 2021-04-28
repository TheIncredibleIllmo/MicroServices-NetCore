using ServiceStore.RabbitMQ.Bus.Queues;
using ServiceStore.RabbitMQ.Bus.RabbitBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Author.Handlers
{
    public class EmailEventHandler : IEventHandler<EmailEventQueue>
    {
        public EmailEventHandler()
        {

        }

        public Task Handle(EmailEventQueue @event)
        {
            return Task.CompletedTask;
        }
    }
}
