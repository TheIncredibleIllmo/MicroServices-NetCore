using Microsoft.Extensions.Logging;
using ServicesStore.Api.Gateway.Interfaces;
using ServicesStore.Api.Gateway.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesStore.Api.Gateway.MessageHandler
{
    public class BooksHandler : DelegatingHandler
    {
        private readonly ILogger<BooksHandler> _logger;
        private readonly IAuthorService _authorService;

        public BooksHandler(ILogger<BooksHandler> logger, IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            var response = await base.SendAsync(request, ct);
            if (!response.IsSuccessStatusCode)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<BookRemote>(content, options);
            if (result == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }

            var authorResponse = await _authorService.GetAuthor(result.BookAuthorGuid ?? Guid.Empty);
            if(!authorResponse.result)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }

            result.Author = authorResponse.author;
            response.Content = new StringContent(JsonSerializer.Serialize(result), System.Text.Encoding.UTF8,"application/json");
            return response;

        }
    }
}
