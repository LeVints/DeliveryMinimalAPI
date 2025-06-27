using DeliveryMinimalAPI.Models;

namespace DeliveryMinimalAPI.DTOs
{
    public class OrderWithDeliveryStatus
    {
        public Order Order { get; set; } = null!;
        public DeliveryStateEnum? CurrentStatus { get; set; }
        public List<DeliveryState> DeliveryHistory { get; set; } = new();
        public DateTime? LastUpdated { get; set; }
    }
} 