using DeliveryMinimalAPI.Models;

namespace DeliveryMinimalAPI.DTOs
{
    public class CreateDeliveryStateRequest
    {
        public int OrderId { get; set; }
        public DeliveryStateEnum State { get; set; }
        public int? DeliveryServiceId { get; set; }
    }
} 