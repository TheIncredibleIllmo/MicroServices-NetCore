using MediatR;
using Microsoft.EntityFrameworkCore;
using ServicesStore.Api.CartService.Common;
using ServicesStore.Api.CartService.DTOs;
using ServicesStore.Api.CartService.Persistence;
using ServicesStore.Api.CartService.RemoteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesStore.Api.CartService.Application
{
    public class GetSingle
    {
        public class Execute: IRequest<CartDto>
        {
            public int CartSessionId { get; set; }
        }

        public class Manejador : BaseHandler,IRequestHandler<Execute, CartDto>
        {
            private readonly IBooksService _booksService;

            public Manejador(CartContext context, IBooksService booksService): base(context,null)
            {
                _booksService = booksService;
            }

            public async Task<CartDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var cartSession = await _context.CartSession.FirstOrDefaultAsync(x => x.CartSessionId == request.CartSessionId);
                var cartSessionDetails = await _context.CartSessionDetail.Where(x => x.CartSessionId == request.CartSessionId).ToListAsync();

                var cartsessionDetailDtos = new List<CartSessionDetailDto>();
                foreach (var cartSessionDetail in cartSessionDetails)
                {
                    var response = await _booksService.GetBook(new Guid(cartSessionDetail.BookGuid));
                    if (response.result)
                    {
                        var book = response.book;
                        cartsessionDetailDtos.Add(new CartSessionDetailDto
                        {
                            BookTitle= book.Title,
                            PublishDate = book.PublishDate,
                            //TODO: book Author.
                            BookGuid = book.LibraryMaterialId,
                        });
                    }

                }

                return new CartDto
                {
                    CartId = cartSession.CartSessionId, 
                    CreatedAt = cartSession.CreatedAt,
                    Details = cartsessionDetailDtos
                };
            }
        }
    }
}
