using Microsoft.AspNetCore.Mvc;
using DeliveryMinimalAPI.Models;
using DeliveryMinimalAPI.DTOs;

namespace DeliveryMinimalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly List<Customer> _customers;
        private readonly List<Order> _orders;
        private readonly List<DeliveryState> _deliveryStates;

        public CustomerController(List<Customer> customers, List<Order> orders, List<DeliveryState> deliveryStates)
        {
            _customers = customers;
            _orders = orders;
            _deliveryStates = deliveryStates;
        }

        // POST: api/customer - Klant aanmaken
        [HttpPost]
        public ActionResult<Customer> Create([FromBody] Customer customer)
        {
            if (customer == null) return BadRequest();
            
            customer.Id = _customers.Count > 0 ? _customers.Max(c => c.Id) + 1 : 1;
            customer.Active = true; // Default to active
            _customers.Add(customer);
            
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        // GET: api/customer/{id} - Klant ophalen
        [HttpGet("{id}")]
        public ActionResult<Customer> GetById(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        // GET: api/customer/{id}/orders - Orders van klant met status
        [HttpGet("{id}/orders")]
        public ActionResult<CustomerOrdersWithStatusResponse> GetCustomerOrdersWithStatus(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();

            var customerOrders = _orders.Where(o => o.CustomerId == id).ToList();
            var ordersWithStatus = new List<OrderWithDeliveryStatus>();

            foreach (var order in customerOrders)
            {
                var latestDeliveryState = _deliveryStates
                    .Where(ds => ds.OrderId == order.Id)
                    .OrderByDescending(ds => ds.DateTime)
                    .FirstOrDefault();
                
                var allDeliveryStates = _deliveryStates
                    .Where(ds => ds.OrderId == order.Id)
                    .OrderBy(ds => ds.DateTime)
                    .ToList();

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
            return new List<Customer>(); // This will be empty now, but other controllers can inject the service
        }
    }
} 