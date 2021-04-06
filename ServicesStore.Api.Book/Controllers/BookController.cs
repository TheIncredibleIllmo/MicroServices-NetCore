using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesStore.Api.Book.Application;
using ServicesStore.Api.Book.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LibraryMaterialDto>>> GetAll()
        {
            return await _mediator.Send(new GetAll.Execute());
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<LibraryMaterialDto>> GetSingle(Guid guid)
        {
            return await _mediator.Send(new GetSingle.Execute { BookGuid = guid });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Execute request)
        {
            return await _mediator.Send(request);
        }
    }
}
