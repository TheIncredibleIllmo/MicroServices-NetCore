using ServicesStore.Api.Gateway.Interfaces;
using ServicesStore.Api.Gateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServicesStore.Api.Gateway.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IHttpClientFactory _httpClient;
        public AuthorService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool result, AuthorRemote author, string)> GetAuthor(Guid id)
        {
            try
            {
                var client = _httpClient.CreateClient("AuthorService");
                var response = await client.GetAsync($"/api/authors/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return (false, null, response.ReasonPhrase);
                }

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<AuthorRemote>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result == null)
                {
                    return (false, null, "Error pulling your author");

                }

                return (true, result, string.Empty);


            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }
    }
}
