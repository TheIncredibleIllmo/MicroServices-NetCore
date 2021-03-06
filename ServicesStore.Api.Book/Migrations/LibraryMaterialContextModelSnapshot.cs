// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServicesStore.Api.BookService.Persistence;

namespace ServicesStore.Api.Book.Migrations
{
    [DbContext(typeof(LibraryMaterialContext))]
    partial class LibraryMaterialContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ServicesStore.Api.Book.Models.LibraryMaterial", b =>
                {
                    b.Property<Guid?>("LibraryMaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookAuthorGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LibraryMaterialId");

                    b.ToTable("LibraryMaterial");
                });
#pragma warning restore 612, 618
        }
    }
}
