using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesStore.Api.Author.Application;
using ServicesStore.Api.Author.DTOs;
using ServicesStore.Api.Author.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Author.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookAuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookAuthorDto>>> GetAll()
        {
            return await _mediator.Send(new GetAll.Execute());
        }


        [HttpGet("{guid}")]
        public async Task<ActionResult<BookAuthorDto>> GetSingle(string guid)
        {
            return await _mediator.Send(new GetSingle.Execute { BookAuthorGuid = guid});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Execute request)
        {
            return await _mediator.Send(request);
        }
    }
}
