using Microsoft.EntityFrameworkCore;
using ServicesStore.Api.CartService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.CartService.Persistence
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options): base(options)
        {

        }

        public DbSet<CartSession> CartSession { get; set; }
        public DbSet<CartSessionDetail> CartSessionDetail { get; set; }

    }
}
