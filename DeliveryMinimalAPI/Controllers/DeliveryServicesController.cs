using Microsoft.AspNetCore.Mvc;
using DeliveryMinimalAPI.Models;

namespace DeliveryMinimalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryServicesController : ControllerBase
    {
        private static readonly List<DeliveryService> Services = new()
        {
            new DeliveryService { Id = 1, Name = "FastExpress" },
            new DeliveryService { Id = 2, Name = "QuickShip" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAll()
        {
            return Ok(Services.Select(s => s.Name));
        }

        [HttpGet("{apiKey}")]
        public ActionResult<DeliveryService> GetByApiKey(string apiKey)
        {
            var service = Services.FirstOrDefault(s => s.Name.Equals(apiKey, StringComparison.OrdinalIgnoreCase));
            if (service == null) return NotFound();
            return Ok(service);
        }

        [HttpGet("IsAuthenticationOk")]
        public ActionResult<bool> IsAuthenticationOk()
        {
            return Ok(true);
        }
    }
} 