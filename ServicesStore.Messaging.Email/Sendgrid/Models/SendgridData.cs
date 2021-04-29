using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesStore.Messaging.Email.Sendgrid.Models
{
    public class SendgridData
    {
        public string SendgridApiSecret { get; set; }

        public string AddresseeEmail { get; set; }

        public string AddresseeName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
