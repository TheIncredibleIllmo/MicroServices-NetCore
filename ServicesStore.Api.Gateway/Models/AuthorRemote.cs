using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Gateway.Models
{
    public class AuthorRemote
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string BookAuthorGuid { get; set; }
    }
}
