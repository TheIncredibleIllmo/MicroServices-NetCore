using AutoMapper;
using FluentValidation;
using MediatR;
using ServicesStore.Api.CartService.Common;
using ServicesStore.Api.CartService.Models;
using ServicesStore.Api.CartService.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesStore.Api.CartService.Application
{
    public class Create
    {
        public class Execute : IRequest
        {
            public DateTime CreatedAt { get; set; }
            public List<string> BooksGuid { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.CreatedAt).NotEmpty();
                RuleFor(x => x.BooksGuid).NotNull();
            }
        }


        public class Handler : BaseHandler,IRequestHandler<Execute>
        {
            public Handler(CartContext context, IMapper mapper) : base(context, mapper)
            {

            }


            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var cartSession = new CartSession
                {
                    CreatedAt = request.CreatedAt
                };

                var result = await _context.CartSession.AddAsync(cartSession);
                if (result?.Entity == null)
                {
                    throw new Exception("Error while adding new cart session.");
                }


                var transactions = await _context.SaveChangesAsync();
                if (transactions == 0)
                {
                    throw new Exception("Error while saving cart session.");

                }

                int cartSessionId = cartSession.CartSessionId;

                foreach (var bookGuid in request.BooksGuid)
                {
                    var detail = new CartSessionDetail
                    {
                        CreatedAt = DateTime.Now,
                        CartSessionId = cartSessionId,
                        BookGuid = bookGuid
                    };

                    var detailResult = await _context.CartSessionDetail.AddAsync(detail);
                    if (detailResult?.Entity == null)
                    {
                        throw new Exception("Error while adding new cart session detail.");
                    }
                }

                transactions = await _context.SaveChangesAsync();
                if (transactions == 0)
                {
                    throw new Exception("Error while saving cart session detail.");

                }


                return Unit.Value;
            }
        }
    }
}
