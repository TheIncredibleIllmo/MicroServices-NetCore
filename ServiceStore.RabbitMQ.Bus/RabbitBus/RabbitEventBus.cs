using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceStore.RabbitMQ.Bus.Commands;
using ServiceStore.RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var factory = new ConnectionFactory() { HostName = "rabbit-mq-server-web" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var eventName = @event.GetType().Name;

                //creates the Queue
                var queueName = channel.QueueDeclare(eventName, false, false, false, null).QueueName;

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                //publish the Queue
                channel.BasicPublish("", queueName, null, body);

            }

        }

        public void Subscribe<T, TU>()
            where T : Event
            where TU : IEventHandler<T>
        {

            var eventName = typeof(T).Name;
            var handlerType = typeof(TU);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }


            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }


            if (_handlers[eventName].Any(x => x.GetType() == handlerType))
            {
                throw new ArgumentException($"The handler {handlerType.Name} was already registered by the {eventName}");
            }

            _handlers[eventName].Add(handlerType);


            //connects to RabbitMQ container

            var factory = new ConnectionFactory() { HostName = "rabbit-mq-server-web", DispatchConsumersAsync = true };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var queueName = channel.QueueDeclare(eventName, false, false, false, null).QueueName;

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += OnReceived;

            channel.BasicConsume(queueName, true, consumer);


        }

        private async Task OnReceived(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
                if (_handlers.ContainsKey(eventName))
                {
                    var subscriptions = _handlers[eventName];
                    foreach (var subscription in subscriptions)
                    {
                        var handler = Activator.CreateInstance(subscription);
                        if (handler == null) continue;

                        var eventType = _eventTypes.SingleOrDefault(x => x.Name == eventName);

                        var deserializedEvent = JsonConvert.DeserializeObject(message, eventType);

                        //Casting
                        var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                        await(Task)concreteType.GetMethod("Handle")?.Invoke(handler, new object[] { deserializedEvent });
                    }
                }
            }
            catch (Exception ex)
            {

                var m = ex.Message;
            }
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

    }
}
