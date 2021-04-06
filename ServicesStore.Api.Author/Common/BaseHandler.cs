using AutoMapper;
using ServicesStore.Api.Author.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Author.Common
{
    public class BaseHandler
    {
        protected readonly BookAuthorContext _context;
        protected readonly IMapper _mapper;

        public BaseHandler(BookAuthorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
