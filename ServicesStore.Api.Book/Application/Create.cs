using AutoMapper;
using FluentValidation;
using MediatR;
using ServicesStore.Api.Book.Common;
using ServicesStore.Api.Book.Models;
using ServicesStore.Api.BookService.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesStore.Api.Book.Application
{
    public class Create
    {
        public class Execute : IRequest
        {
            public string Title { get; set; }
            public DateTime? PublishDate { get; set; }
            public Guid? BookAuthorGuid { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.PublishDate).NotEmpty();
                RuleFor(x => x.BookAuthorGuid).NotEmpty();
            }
        }

        public class Handler : BaseHandler,IRequestHandler<Execute>
        {
            public Handler(LibraryMaterialContext context) : base(context,null)
            {

            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var newBook = new LibraryMaterial
                {
                    Title = request.Title,
                    PublishDate = request.PublishDate,
                    BookAuthorGuid = request.BookAuthorGuid
                };

                var result = _context.LibraryMaterial.Add(newBook);
                if (result?.Entity == null)
                {
                    throw new Exception("Error while adding new Book.");
                }

                var transactions = await _context.SaveChangesAsync();
                if (transactions > 0) return Unit.Value;

                throw new Exception("Error while saving Book.");

            }
        }
    }
}
