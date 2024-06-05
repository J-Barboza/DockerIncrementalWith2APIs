using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CallerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncrementController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IncrementController> _logger;

        public IncrementController(IHttpClientFactory httpClientFactory, ILogger<IncrementController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> IncrementCounter()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsync("http://localhost:5211/api/counter", null);
            if (response.IsSuccessStatusCode)
            {
                var counterValue = await response.Content.ReadAsStringAsync();
                return Ok($"Contagem incrementada com sucesso. Novo valor: {counterValue}");
            }

            return StatusCode((int)response.StatusCode, "Falha ao incrementar o contador");
        }
    }
}
