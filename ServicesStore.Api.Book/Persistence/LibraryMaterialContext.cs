using Microsoft.EntityFrameworkCore;
using ServicesStore.Api.Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.BookService.Persistence
{
    public class LibraryMaterialContext : DbContext
    {
        public LibraryMaterialContext()
        {

        }

        public LibraryMaterialContext(DbContextOptions<LibraryMaterialContext> options): base(options)
        {

        }

        // virtual for testing overwrite.
        public virtual DbSet<LibraryMaterial> LibraryMaterial { get; set; }
    }
}
