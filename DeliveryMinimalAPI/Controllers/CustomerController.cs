using Microsoft.AspNetCore.Mvc;
using DeliveryMinimalAPI.Models;

namespace DeliveryMinimalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private static readonly List<Customer> Customers = new();

        // GET: api/customer
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            return Ok(Customers);
        }

        // GET: api/customer/{id}
        [HttpGet("{id}")]
        public ActionResult<Customer> GetById(int id)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        // POST: api/customer
        [HttpPost]
        public ActionResult<Customer> Create([FromBody] Customer customer)
        {
            if (customer == null) return BadRequest();
            
            customer.Id = Customers.Count > 0 ? Customers.Max(c => c.Id) + 1 : 1;
            customer.Active = true; // Default to active
            Customers.Add(customer);
            
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        // PUT: api/customer/{id} - Volledige update
        [HttpPut("{id}")]
        public ActionResult<Customer> Update(int id, [FromBody] Customer customer)
        {
            if (customer == null) return BadRequest();
            
            var existingCustomer = Customers.FirstOrDefault(c => c.Id == id);
            if (existingCustomer == null) return NotFound();

            // Update alle velden
            existingCustomer.Name = customer.Name;
            existingCustomer.Address = customer.Address;
            existingCustomer.Active = customer.Active;
            existingCustomer.Orders = customer.Orders;

            return Ok(existingCustomer);
        }

        // PATCH: api/customer/{id} - Gedeeltelijke update
        [HttpPatch("{id}")]
        public ActionResult<Customer> PartialUpdate(int id, [FromBody] CustomerUpdateRequest request)
        {
            var existingCustomer = Customers.FirstOrDefault(c => c.Id == id);
            if (existingCustomer == null) return NotFound();

            // Update alleen de velden die zijn opgegeven
            if (request.Name != null)
                existingCustomer.Name = request.Name;
            
            if (request.Address != null)
                existingCustomer.Address = request.Address;
            
            if (request.Active.HasValue)
                existingCustomer.Active = request.Active.Value;

            return Ok(existingCustomer);
        }

        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();

            Customers.Remove(customer);
            return NoContent();
        }
    }

    // DTO voor gedeeltelijke updates
    public class CustomerUpdateRequest
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public bool? Active { get; set; }
    }
} 