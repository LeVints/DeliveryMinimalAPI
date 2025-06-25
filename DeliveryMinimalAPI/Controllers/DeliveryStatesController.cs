using Microsoft.AspNetCore.Mvc;
using DeliveryMinimalAPI.Models;

namespace DeliveryMinimalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryStatesController : ControllerBase
    {
        private static readonly List<DeliveryState> States = new();

        [HttpGet("DeliveryStates")]
        public ActionResult<IEnumerable<DeliveryState>> GetDeliveryStates()
        {
            return Ok(States);
        }

        [HttpGet("GetAllDeliveryStates")]
        public ActionResult<IEnumerable<DeliveryState>> GetAllDeliveryStates()
        {
            return Ok(States);
        }

        [HttpPost("StartDelivery")]
        public ActionResult<IEnumerable<DeliveryState>> StartDelivery([FromQuery] int OrderId)
        {
            var state = new DeliveryState { Id = States.Count + 1, OrderId = OrderId, State = DeliveryStateEnum.State1, DateTime = DateTime.UtcNow };
            States.Add(state);
            return Ok(States);
        }

        [HttpPost("UpdateDeliveryState")]
        public ActionResult<IEnumerable<DeliveryState>> UpdateDeliveryState([FromBody] DeliveryState deliveryState)
        {
            var state = States.FirstOrDefault(s => s.Id == deliveryState.Id);
            if (state != null)
            {
                state.State = deliveryState.State;
                state.DateTime = deliveryState.DateTime;
            }
            return Ok(States);
        }

        [HttpPost("CompleteDelivery")]
        public ActionResult<IEnumerable<DeliveryState>> CompleteDelivery([FromQuery] int OrderId)
        {
            var state = new DeliveryState { Id = States.Count + 1, OrderId = OrderId, State = DeliveryStateEnum.State4, DateTime = DateTime.UtcNow };
            States.Add(state);
            return Ok(States);
        }
    }
} 