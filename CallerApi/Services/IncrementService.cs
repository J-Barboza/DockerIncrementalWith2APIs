using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CallerApi.Services
{
    public class IncrementService : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IncrementService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _incrementApiUrl;

        public IncrementService(IHttpClientFactory httpClientFactory, ILogger<IncrementService> logger, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _configuration = configuration;
            _incrementApiUrl = Environment.GetEnvironmentVariable("IncrementApiUrl") ?? "http://incrementapi/api/counter";
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var interval = _configuration.GetValue<int>("IncrementInterval");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Incrementando contador em: {time}", DateTimeOffset.Now);
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.PostAsync(_incrementApiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    var counterValue = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("Contador incrementado com sucesso. Novo valor: {counterValue}", counterValue);
                }
                else
                {
                    _logger.LogError("Falha ao incrementar contador. Status code: {statusCode}", response.StatusCode);
                }

                await Task.Delay(interval * 1000, stoppingToken);
            }
        }
    }
}
