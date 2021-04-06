using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServicesStore.Api.Author.Models;

namespace ServicesStore.Api.Author.Persistence
{
    public class BookAuthorContext : DbContext
    {
        public BookAuthorContext(DbContextOptions<BookAuthorContext> options) : base(options)
        {
        }

        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<AcademicDegree> AcademicDegree { get; set; }
    }
}
