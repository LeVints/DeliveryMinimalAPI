using System;
using System.Collections.Generic;

namespace DeliveryMinimalAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public IReadOnlyList<Product>? Products { get; set; }
        public IReadOnlyList<DeliveryState>? DeliveryStates { get; set; }
    }
} 