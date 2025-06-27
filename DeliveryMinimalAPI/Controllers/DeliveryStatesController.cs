using Microsoft.AspNetCore.Mvc;
using DeliveryMinimalAPI.Models;
using DeliveryMinimalAPI.DTOs;

namespace DeliveryMinimalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryStatesController : ControllerBase
    {
        private static readonly List<DeliveryState> States = new();

        // POST: api/deliverystates - Nieuwe delivery status toevoegen
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
            return Ok(state);
        }

        // PUT: api/deliverystates/{id} - Delivery status bijwerken
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
            return Ok(state);
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
            return Ok(state);
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
            return Ok(state);
        }

        // Static methods om delivery states te delen met andere controllers
        public static List<DeliveryState> GetAllDeliveryStates()
        {
            return States;
        }

        public static List<DeliveryState> GetDeliveryStatesByOrderId(int orderId)
        {
            return States.Where(s => s.OrderId == orderId).OrderBy(s => s.DateTime).ToList();
        }

        public static DeliveryState? GetLatestDeliveryState(int orderId)
        {
            return States.Where(s => s.OrderId == orderId).OrderByDescending(s => s.DateTime).FirstOrDefault();
        }
    }
} 