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
    public class GetAll
    {
        public class Execute : IRequest<List<BookAuthorDto>>
        {

        }

        public class Handler : BaseHandler,IRequestHandler<Execute, List<BookAuthorDto>>
        {
            public Handler(BookAuthorContext context,IMapper mapper) : base(context,mapper)
            {
            }

            public async Task<List<BookAuthorDto>> Handle(Execute request, CancellationToken cancellationToken)
            {
               var dbBookAuthors = await _context.BookAuthor.ToListAsync();
               var bookAuthorsDto = _mapper.Map<List<BookAuthor>, List<BookAuthorDto>>(dbBookAuthors);
                if (bookAuthorsDto == null) throw new Exception("Error while mapping dbBookAuthor to its Dto.");
               return bookAuthorsDto;
            }
        }
    }
}
