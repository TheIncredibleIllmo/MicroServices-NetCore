using Microsoft.Extensions.Logging;
using ServicesStore.Messaging.Email.Sendgrid.Interfaces;
using ServicesStore.Messaging.Email.Sendgrid.Models;
using ServiceStore.RabbitMQ.Bus.Queues;
using ServiceStore.RabbitMQ.Bus.RabbitBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace ServicesStore.Api.Author.Handlers
{
    public class EmailEventHandler : IEventHandler<EmailEventQueue>
    {
        private readonly ILogger<EmailEventHandler> _logger;
        private readonly ISendgridSend _sendgridSend;
        private readonly IConfiguration _configuration;

        public EmailEventHandler(ILogger<EmailEventHandler> logger, ISendgridSend sendgridSend, IConfiguration configuration)
        {
            _logger = logger;
            _sendgridSend = sendgridSend;
            _configuration = configuration;
        }

        public async Task Handle(EmailEventQueue @event)
        {

            var sendgridData = new SendgridData()
            {
                SendgridApiSecret = _configuration["Sendgrid:ApiSecret"],
                AddresseeName = @event.Addressee,
                AddresseeEmail= @event.Addressee,
                Title= @event.Title,
                Content= @event.Content
            };

            var result = await _sendgridSend.SendEmail(sendgridData);
            if (result.result)
            {
                await Task.CompletedTask;
                return;
            }
        }
    }
}
