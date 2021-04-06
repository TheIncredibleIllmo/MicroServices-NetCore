using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServicesStore.Api.Book.Application;
using ServicesStore.Api.Book.DTOs;
using ServicesStore.Api.Book.Models;
using ServicesStore.Api.BookService.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ServicesStore.Api.BooksService.Tests
{
    public class BooksServiceTest
    {

        private IEnumerable<LibraryMaterial> GetLibraryMaterials()
        {
            //configures genfu to fill with fake data
            A.Configure<LibraryMaterial>()
                .Fill(x => x.Title).AsArticleTitle()
                .Fill(x => x.LibraryMaterialId, () => Guid.NewGuid());

            var books = A.ListOf<LibraryMaterial>(30);
            books[0].LibraryMaterialId = Guid.Empty;

            return books;
        }

        private Mock<LibraryMaterialContext> CreateLibraryMaterialContext()
        {
            var books = GetLibraryMaterials().AsQueryable();

            //Indicates LibraryMaterial is an Entity class.
            //it needs to be done manually since we don't have a persistence class
            // like LibraryMaterial Context.
            var dbSet = new Mock<DbSet<LibraryMaterial>>();
            dbSet.As<IQueryable<LibraryMaterial>>().Setup(x => x.Provider).Returns(books.Provider);
            dbSet.As<IQueryable<LibraryMaterial>>().Setup(x => x.Expression).Returns(books.Expression);
            dbSet.As<IQueryable<LibraryMaterial>>().Setup(x => x.ElementType).Returns(books.ElementType);
            dbSet.As<IQueryable<LibraryMaterial>>().Setup(x => x.GetEnumerator()).Returns(books.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibraryMaterial>>()
                .Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibraryMaterial>(books.GetEnumerator()));

            dbSet.As<IQueryable<LibraryMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibraryMaterial>(books.Provider));

            var context = new Mock<LibraryMaterialContext>();
            context.Setup(x=>x.LibraryMaterial).Returns(dbSet.Object);
            return context;
        }

        private IMapper CreateMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));
            return mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetBooks()
        {
            //emulate context
            var mockContext = CreateLibraryMaterialContext();

            //emulate IMapper
            var mapper = CreateMapper();

            //emulate Handler
            var handler = new GetAll.Handler(mockContext.Object, mapper);
            var request = new GetAll.Execute();

            var books = await handler.Handle(request, CancellationToken.None);

            Assert.True(books.Any());
        }

        [Fact]
        public async Task GetBook()
        {
            var mockContext = CreateLibraryMaterialContext();
            var mapper = CreateMapper();

            var request = new GetSingle.Execute();
            request.BookGuid = Guid.Empty;

            var handler = new GetSingle.Handler(mockContext.Object, mapper);
            var book =await handler.Handle(request, new CancellationToken());

            Assert.NotNull(book);
            Assert.True(book.LibraryMaterialId == Guid.Empty);
        }

        [Fact]
        public async Task CreateBook()
        {
            // when Debugging is required, System.Diagnostics.Debugger.Launch();

            var options = new DbContextOptionsBuilder<LibraryMaterialContext>()
                .UseInMemoryDatabase(databaseName: "BookStore")
                .Options;


            var context = new LibraryMaterialContext(options);
            var request = new Create.Execute();
            request.Title = "Net Core Micro-services";
            request.BookAuthorGuid = Guid.Empty;
            request.PublishDate = DateTime.Now;

            var handler = new Create.Handler(context);
            var result = await handler.Handle(request, new CancellationToken());

            Assert.True(result!=null);

        }
    }
}
