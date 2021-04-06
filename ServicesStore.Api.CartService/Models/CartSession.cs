using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.CartService.Models
{
    public class CartSession
    {
        public int CartSessionId { get; set; }
        public DateTime? CreatedAt { get; set; }

        //relations
        public ICollection<CartSessionDetail> Details { get; set; }
    }
}
