using DeliveryMinimalAPI.Models;

namespace DeliveryMinimalAPI.DTOs
{
    public class CustomerOrdersWithStatusResponse
    {
        public Customer Customer { get; set; } = null!;
        public List<OrderWithDeliveryStatus> OrdersWithStatus { get; set; } = new();
    }
} 