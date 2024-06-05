using Microsoft.AspNetCore.Mvc;

namespace IncrementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CounterController : ControllerBase
    {
        private static int _counter = 0;
        private static readonly object _lock = new object();

        [HttpGet]
        public IActionResult GetCounter()
        {
            return Ok(_counter);
        }

        [HttpPost]
        public IActionResult IncrementCounter()
        {
            lock (_lock)
            {
                _counter++;
            }
            return Ok(_counter);
        }
    }
}
