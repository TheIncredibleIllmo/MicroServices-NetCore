using ServiceStore.RabbitMQ.Bus.Commands;
using ServiceStore.RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStore.RabbitMQ.Bus.RabbitBus
{
    public interface IRabbitEventBus
    {
        Task SendCommand<T>(T command) where T : Command;
        
        void Publish<T>(T @event) where T: Event;

        void Subscribe<T, TU>() where T : Event where TU : IEventHandler<T>;
    }

    public interface IEventHandler<in T>: IEventHandler where T: Event
    {
        Task Handle(T @event);
    }

    public interface IEventHandler
    {

    }

}
