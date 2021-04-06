using AutoMapper;
using ServicesStore.Api.BookService.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Book.Common
{
    public class BaseHandler
    {
        protected readonly LibraryMaterialContext _context;
        protected readonly IMapper _mapper;

        public BaseHandler(LibraryMaterialContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
