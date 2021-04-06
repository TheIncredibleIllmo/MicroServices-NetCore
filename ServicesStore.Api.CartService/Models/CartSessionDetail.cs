using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.CartService.Models
{
    public class CartSessionDetail
    {
        public int CartSessionDetailId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string BookGuid { get; set; }

        //relationships
        public int CartSessionId { get; set; }
        public CartSession CartSession{ get; set; }
    }
}
