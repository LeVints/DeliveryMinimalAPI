using DeliveryMinimalAPI.Models;

namespace DeliveryMinimalAPI.DTOs
{
    public class UpdateDeliveryStateRequest
    {
        public DeliveryStateEnum? State { get; set; }
        public int? DeliveryServiceId { get; set; }
    }
} 