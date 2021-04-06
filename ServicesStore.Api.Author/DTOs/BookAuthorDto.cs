using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Author.DTOs
{
    public class BookAuthorDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string BookAuthorGuid { get; set; }
    }
}
