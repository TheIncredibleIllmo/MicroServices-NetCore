using AutoMapper;
using FluentValidation;
using MediatR;
using ServicesStore.Api.Author.Common;
using ServicesStore.Api.Author.Models;
using ServicesStore.Api.Author.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesStore.Api.Author.Application
{
    public class Create
    {
        public class Execute : IRequest
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public DateTime? DOB { get; set; }

        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.DOB).NotEmpty();
            }
        }

        public class Handler : BaseHandler, IRequestHandler<Execute>
        {
            public Handler(BookAuthorContext context, IMapper mapper) : base(context, mapper)
            {
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var newBookAuthor = new BookAuthor
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    DOB = request.DOB,
                    BookAuthorGuid = Convert.ToString(Guid.NewGuid())
                };

                var result = await _context.AddAsync(newBookAuthor);
                if (result?.Entity == null) {
                    throw new Exception("Error while adding new BookAuthor.");
                }

                var transactions = await _context.SaveChangesAsync();
                if(transactions > 0) return Unit.Value;

                throw new Exception("Error while saving BookAuthor.");
            }
        }
    }
}
