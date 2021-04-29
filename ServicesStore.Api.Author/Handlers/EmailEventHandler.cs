using Microsoft.Extensions.Logging;
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
        private readonly ILogger<EmailEventHandler> _logger;

        public EmailEventHandler(ILogger<EmailEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(EmailEventQueue @event)
        {
            _logger.LogInformation(@event.Addressee);
        }
    }
}
