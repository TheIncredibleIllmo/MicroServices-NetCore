using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using ServiceStore.RabbitMQ.Bus.Commands;
using ServiceStore.RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStore.RabbitMQ.Bus.RabbitBus
{
    public class RabbitEventBus : IRabbitEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;

        public RabbitEventBus(IMediator mediator)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        public void Publish<T>(T @event) where T : Event
        {
            //connects to RabbitMQ container

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var eventName = @event.GetType().Name;

            //creates the Queue
            channel.QueueDeclare(eventName, false, false, false, null);
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            //publish the Queue
            channel.BasicPublish("", eventName, null, body);

        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }


        public void Subscribe<T, TU>()
            where T : Event
            where TU : IEventHandler<T>
        {
            throw new NotImplementedException();
        }
    }
}
