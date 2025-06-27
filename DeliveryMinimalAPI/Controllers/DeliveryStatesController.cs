using Microsoft.AspNetCore.Mvc;
using DeliveryMinimalAPI.Models;

namespace DeliveryMinimalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryStatesController : ControllerBase
    {
        private static readonly List<DeliveryState> States = new();

        // GET: api/deliverystates
        [HttpGet]
        public ActionResult<IEnumerable<DeliveryState>> GetAll()
        {
            return Ok(States);
        }

        // GET: api/deliverystates/{id}
        [HttpGet("{id}")]
        public ActionResult<DeliveryState> GetById(int id)
        {
            var state = States.FirstOrDefault(s => s.Id == id);
            if (state == null) return NotFound();
            return Ok(state);
        }

        // GET: api/deliverystates/order/{orderId}
        [HttpGet("order/{orderId}")]
        public ActionResult<IEnumerable<DeliveryState>> GetByOrderId(int orderId)
        {
            var orderStates = States.Where(s => s.OrderId == orderId).OrderBy(s => s.DateTime);
            return Ok(orderStates);
        }

        // POST: api/deliverystates
        [HttpPost]
        public ActionResult<DeliveryState> Create([FromBody] CreateDeliveryStateRequest request)
        {
            if (request == null || request.OrderId <= 0)
                return BadRequest("OrderId is required and must be greater than 0");

            var state = new DeliveryState
            {
                Id = States.Count + 1,
                OrderId = request.OrderId,
                State = request.State,
                DateTime = DateTime.UtcNow,
                DeliveryServiceId = request.DeliveryServiceId
            };
            States.Add(state);
            return CreatedAtAction(nameof(GetById), new { id = state.Id }, state);
        }

        // PUT: api/deliverystates/{id}
        [HttpPut("{id}")]
        public ActionResult<DeliveryState> Update(int id, [FromBody] UpdateDeliveryStateRequest request)
        {
            var state = States.FirstOrDefault(s => s.Id == id);
            if (state == null) return NotFound();

            if (request.State.HasValue)
                state.State = request.State.Value;

            if (request.DeliveryServiceId.HasValue)
                state.DeliveryServiceId = request.DeliveryServiceId.Value;

            state.DateTime = DateTime.UtcNow; // Update timestamp

            return Ok(state);
        }

        // DELETE: api/deliverystates/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var state = States.FirstOrDefault(s => s.Id == id);
            if (state == null) return NotFound();

            States.Remove(state);
            return NoContent();
        }

        // Convenience endpoints voor specifieke acties
        [HttpPost("order/{orderId}/start")]
        public ActionResult<DeliveryState> StartDelivery(int orderId, [FromBody] StartDeliveryRequest request)
        {
            var state = new DeliveryState
            {
                Id = States.Count + 1,
                OrderId = orderId,
                State = DeliveryStateEnum.OrderReceived,
                DateTime = DateTime.UtcNow,
                DeliveryServiceId = request.DeliveryServiceId
            };
            States.Add(state);
            return CreatedAtAction(nameof(GetById), new { id = state.Id }, state);
        }

        [HttpPost("order/{orderId}/ship")]
        public ActionResult<DeliveryState> ShipOrder(int orderId)
        {
            var state = new DeliveryState
            {
                Id = States.Count + 1,
                OrderId = orderId,
                State = DeliveryStateEnum.Shipped,
                DateTime = DateTime.UtcNow
            };
            States.Add(state);
            return CreatedAtAction(nameof(GetById), new { id = state.Id }, state);
        }

        [HttpPost("order/{orderId}/deliver")]
        public ActionResult<DeliveryState> DeliverOrder(int orderId)
        {
            var state = new DeliveryState
            {
                Id = States.Count + 1,
                OrderId = orderId,
                State = DeliveryStateEnum.Delivered,
                DateTime = DateTime.UtcNow
            };
            States.Add(state);
            return CreatedAtAction(nameof(GetById), new { id = state.Id }, state);
        }
    }

    // DTO's voor requests
    public class CreateDeliveryStateRequest
    {
        public int OrderId { get; set; }
        public DeliveryStateEnum State { get; set; }
        public int? DeliveryServiceId { get; set; }
    }

    public class UpdateDeliveryStateRequest
    {
        public DeliveryStateEnum? State { get; set; }
        public int? DeliveryServiceId { get; set; }
    }

    public class StartDeliveryRequest
    {
        public int? DeliveryServiceId { get; set; }
    }
} 