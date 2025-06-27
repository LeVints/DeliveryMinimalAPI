using Microsoft.AspNetCore.Mvc;
using DeliveryMinimalAPI.Models;
using DeliveryMinimalAPI.DTOs;

namespace DeliveryMinimalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private static readonly List<Customer> Customers = new();

        // POST: api/customer - Klant aanmaken
        [HttpPost]
        public ActionResult<Customer> Create([FromBody] Customer customer)
        {
            if (customer == null) return BadRequest();
            
            customer.Id = Customers.Count > 0 ? Customers.Max(c => c.Id) + 1 : 1;
            customer.Active = true; // Default to active
            Customers.Add(customer);
            
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        // GET: api/customer/{id} - Klant ophalen
        [HttpGet("{id}")]
        public ActionResult<Customer> GetById(int id)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        // GET: api/customer/{id}/orders - Orders van klant met status
        [HttpGet("{id}/orders")]
        public ActionResult<CustomerOrdersWithStatusResponse> GetCustomerOrdersWithStatus(int id)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();

            var customerOrders = OrderController.GetOrdersByCustomerId(id);
            var ordersWithStatus = new List<OrderWithDeliveryStatus>();

            foreach (var order in customerOrders)
            {
                var latestDeliveryState = DeliveryStatesController.GetLatestDeliveryState(order.Id);
                var allDeliveryStates = DeliveryStatesController.GetDeliveryStatesByOrderId(order.Id);

                ordersWithStatus.Add(new OrderWithDeliveryStatus
                {
                    Order = order,
                    CurrentStatus = latestDeliveryState?.State,
                    DeliveryHistory = allDeliveryStates,
                    LastUpdated = latestDeliveryState?.DateTime
                });
            }

            var response = new CustomerOrdersWithStatusResponse
            {
                Customer = customer,
                OrdersWithStatus = ordersWithStatus
            };

            return Ok(response);
        }

        // Static method om customers te delen met andere controllers
        public static List<Customer> GetAllCustomers()
        {
            return Customers;
        }
    }
} 