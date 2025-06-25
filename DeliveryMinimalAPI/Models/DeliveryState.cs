using System;

namespace DeliveryMinimalAPI.Models
{
    public class DeliveryState
    {
        public int Id { get; set; }
        public DeliveryStateEnum State { get; set; }
        public DateTime DateTime { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int DeliveryServiceId { get; set; }
        public DeliveryService? DeliveryService { get; set; }
    }
} 