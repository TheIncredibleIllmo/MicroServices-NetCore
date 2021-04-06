using AutoMapper;
using ServicesStore.Api.CartService.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.CartService.Common
{
    public class BaseHandler
    {
        protected readonly CartContext _context;
        protected readonly IMapper _mapper;

        public BaseHandler(CartContext context, IMapper? mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
