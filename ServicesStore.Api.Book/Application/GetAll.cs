using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServicesStore.Api.Book.Common;
using ServicesStore.Api.Book.DTOs;
using ServicesStore.Api.Book.Models;
using ServicesStore.Api.BookService.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesStore.Api.Book.Application
{
    public class GetAll
    {
        public class Execute : IRequest<List<LibraryMaterialDto>>
        {

        }

        public class Handler : BaseHandler,IRequestHandler<Execute, List<LibraryMaterialDto>>
        {
            public Handler(LibraryMaterialContext context, IMapper mapper) : base(context, mapper, null)
            {
            }


            public async Task<List<LibraryMaterialDto>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var dbBooks = await _context.LibraryMaterial.ToListAsync();
                var booksDto = _mapper.Map<List<LibraryMaterial>, List<LibraryMaterialDto>>(dbBooks);
                if (booksDto == null) throw new Exception("Error while mapping dbBooks to its dto.");

                return booksDto;
            }
        }
    }
}
