using Microsoft.Extensions.Logging;
using ServicesStore.Api.CartService.RemoteInterfaces;
using ServicesStore.Api.CartService.RemoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServicesStore.Api.CartService.RemoteServices
{
    public class BooksService : IBooksService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<BooksService> _logger;

        public BooksService(IHttpClientFactory httpClient, ILogger<BooksService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool result, BookRemote book, string errorMessage)> GetBook(Guid guid)
        {
            try
            {
                var client = _httpClient.CreateClient("Books");
                var response = await client.GetAsync($"api/book/{guid}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<BookRemote>(content, options);
                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
