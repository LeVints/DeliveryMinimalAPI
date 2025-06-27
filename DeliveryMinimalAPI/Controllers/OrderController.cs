using Microsoft.AspNetCore.Mvc;
using DeliveryMinimalAPI.Models;
using DeliveryMinimalAPI.DTOs;

namespace DeliveryMinimalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private static readonly List<Order> Orders = new();

        // POST: api/order - Order aanmaken met klant
        [HttpPost]
        public ActionResult<Order> Create([FromBody] CreateOrderRequest request)
        {
            if (request == null || request.CustomerId <= 0)
                return BadRequest("CustomerId is required and must be greater than 0");

            var order = new Order 
            { 
                Id = Orders.Count + 1, 
                OrderDate = DateTime.UtcNow,
                CustomerId = request.CustomerId
            };
            Orders.Add(order);
            return Ok(order);
        }

        // Static methods om orders te delen met andere controllers
        public static List<Order> GetAllOrders()
        {
            return Orders;
        }

        public static List<Order> GetOrdersByCustomerId(int customerId)
        {
            return Orders.Where(o => o.CustomerId == customerId).ToList();
        }
    }
} 