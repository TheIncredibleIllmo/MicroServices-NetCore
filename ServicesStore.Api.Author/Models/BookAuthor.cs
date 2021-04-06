using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Author.Models
{
    public class BookAuthor
    {
        public int BookAuthorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }

        //Relationships
        public ICollection<AcademicDegree> AcademicDegrees { get; set; }

        //Micro services
        public string BookAuthorGuid { get; set; }
    }
}
