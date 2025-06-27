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

        // POST: api/order - Order aanmaken zonder klant
        [HttpPost]
        public ActionResult<Order> Create()
        {
            var order = new Order { Id = Orders.Count + 1, OrderDate = DateTime.UtcNow };
            Orders.Add(order);
            return Ok(order);
        }

        // POST: api/order/with-customer - Order aanmaken met klant
        [HttpPost("with-customer")]
        public ActionResult<Order> CreateWithCustomer([FromBody] CreateOrderRequest request)
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

        [HttpGet("{id}")]
        public ActionResult<Order> GetById(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // PUT: api/order/{id} - Order volledig bijwerken
        [HttpPut("{id}")]
        public ActionResult<Order> Update(int id, [FromBody] UpdateOrderRequest request)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            if (request.CustomerId.HasValue)
                order.CustomerId = request.CustomerId.Value;

            if (request.OrderDate.HasValue)
                order.OrderDate = request.OrderDate.Value;

            return Ok(order);
        }

        // PATCH: api/order/{id}/customer - Alleen klant toevoegen/wijzigen
        [HttpPatch("{id}/customer")]
        public ActionResult<Order> UpdateCustomer(int id, [FromBody] UpdateCustomerRequest request)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            if (request.CustomerId <= 0)
                return BadRequest("CustomerId must be greater than 0");

            order.CustomerId = request.CustomerId;
            return Ok(order);
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            Orders.Remove(order);
            return NoContent();
        }
    }

    // DTO's voor requests
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
    }

    public class UpdateOrderRequest
    {
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
    }

    public class UpdateCustomerRequest
    {
        public int CustomerId { get; set; }
    }
} 