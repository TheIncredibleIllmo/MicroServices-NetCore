using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.CartService.DTOs
{
    public class CartSessionDetailDto
    {
        public Guid? BookGuid { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public DateTime? PublishDate{ get; set; }
    }
}
