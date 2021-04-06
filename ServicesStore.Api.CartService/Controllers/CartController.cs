using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesStore.Api.CartService.Application;
using ServicesStore.Api.CartService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Execute request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> GetSingle(int id)
        {
            return await _mediator.Send(new GetSingle.Execute { CartSessionId = id});
        }
    }
}
