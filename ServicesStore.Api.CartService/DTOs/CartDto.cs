using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.CartService.DTOs
{
    public class CartDto
    {
        public int CartId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<CartSessionDetailDto> Details { get; set; }
    }
}
