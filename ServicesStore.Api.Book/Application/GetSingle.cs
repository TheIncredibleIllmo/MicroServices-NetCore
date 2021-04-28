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
    public class GetSingle
    {
        public class Execute : IRequest<LibraryMaterialDto>
        {
            public Guid? BookGuid { get; set; }

        }

        public class Handler : BaseHandler, IRequestHandler<Execute, LibraryMaterialDto>
        {
            public Handler(LibraryMaterialContext context, IMapper mapper) : base(context, mapper,null)
            {
            }


            public async Task<LibraryMaterialDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var dbBook = await _context.LibraryMaterial.Where(x=> x.LibraryMaterialId == request.BookGuid)?.FirstOrDefaultAsync();
                var bookDto = _mapper.Map<LibraryMaterial, LibraryMaterialDto>(dbBook);
                if (bookDto == null) throw new Exception("Error while mapping dbBook to its dto.");

                return bookDto;
            }
        }
    }
}
