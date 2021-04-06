using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServicesStore.Api.Author.Common;
using ServicesStore.Api.Author.DTOs;
using ServicesStore.Api.Author.Models;
using ServicesStore.Api.Author.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesStore.Api.Author.Application
{
    public class GetSingle
    {
        public class Execute: IRequest<BookAuthorDto> 
        {
            public string BookAuthorGuid { get; set; }
        }

        public class Handler : BaseHandler,IRequestHandler<Execute, BookAuthorDto>
        {
            public Handler(BookAuthorContext context, IMapper mapper) : base(context, mapper)
            {
            }

            public async Task<BookAuthorDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var dbBookAuthor = await _context.BookAuthor
                    .Where(x => x.BookAuthorGuid == request.BookAuthorGuid)
                    .FirstOrDefaultAsync();

                if (dbBookAuthor == null) throw new Exception("BookAuthor not found.");

                var bookAuthorDto = _mapper.Map<BookAuthor, BookAuthorDto>(dbBookAuthor);
                if (bookAuthorDto == null) throw new Exception("Error while mapping bookAuthor to its dto.");

                return bookAuthorDto;
            }
        }
    }
}
