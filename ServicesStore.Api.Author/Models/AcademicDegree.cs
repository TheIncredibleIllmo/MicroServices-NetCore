using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Author.Models
{
    public class AcademicDegree
    {
        public int AcademicDegreeId { get; set; }
        public string Name { get; set; }
        public string AcademicCenter { get; set; }
        public DateTime? DegreeDate { get; set; }


        //Relationships
        public int BookAuthorId { get; set; }
        public BookAuthor BookAuthor { get; set; }

        //Micro services
        public string AcademicDegreeGuid { get; set; }
    }
}
