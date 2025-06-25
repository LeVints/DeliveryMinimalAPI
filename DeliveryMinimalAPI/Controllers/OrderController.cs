using Microsoft.AspNetCore.Mvc;
using DeliveryMinimalAPI.Models;

namespace DeliveryMinimalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private static readonly List<Order> Orders = new();

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll()
        {
            return Ok(Orders);
        }

        [HttpPost]
        public ActionResult<Order> Create()
        {
            var order = new Order { Id = Orders.Count + 1, OrderDate = DateTime.UtcNow };
            Orders.Add(order);
            return Ok(order);
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetById(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            return Ok(order);
        }
    }
} 