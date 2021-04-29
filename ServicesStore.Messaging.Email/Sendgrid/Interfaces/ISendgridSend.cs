using SendGrid;
using SendGrid.Helpers.Mail;
using ServicesStore.Messaging.Email.Sendgrid.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServicesStore.Messaging.Email.Sendgrid.Interfaces
{
    public interface ISendgridSend
    {
        Task<(bool result, string errorMessage)> SendEmail(SendgridData data);
    }

    public class SendgridSend : ISendgridSend
    {
        public async Task<(bool result, string errorMessage)> SendEmail(SendgridData data)
        {
            try
            {
                var sendgridClient = new SendGridClient(data.SendgridApiSecret);

                var addressee = new EmailAddress(data.AddresseeEmail, data.AddresseeName);

                var sender = new EmailAddress("eduardomedm@gmail.com", "Eduardo Medina");

                var message = MailHelper.CreateSingleEmail(sender, addressee, data.Title, data.Content, data.Content);

                var result = await sendgridClient.SendEmailAsync(message);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    return (false, "Error when sending email...");
                }

                return (true, null);

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }
    }


}
