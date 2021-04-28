using ServiceStore.RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceStore.RabbitMQ.Bus.Queues
{
    public class EmailEventQueue: Event
    {
        public string Addressee { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public EmailEventQueue(string addressee,string title, string content)
        {
            Addressee = addressee;
            Title = title;
            Content = content;
        }
    }
}
