using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesStore.Api.Gateway.MessageHandler
{
    public class BooksHandler : DelegatingHandler
    {
        private readonly ILogger<BooksHandler> _logger;

        public BooksHandler(ILogger<BooksHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            var responseTime = Stopwatch.StartNew();
            _logger.LogInformation("Request started!");
            var response = await base.SendAsync(request, ct);
            _logger.LogInformation($"It took: {responseTime.ElapsedMilliseconds}ms");
            return response;
        }
    }
}
