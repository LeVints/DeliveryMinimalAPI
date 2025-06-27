namespace DeliveryMinimalAPI.Models
{
    public enum DeliveryStateEnum
    {
        OrderReceived = 1,
        Processing = 2,
        Shipped = 3,
        InTransit = 4,
        OutForDelivery = 5,
        Delivered = 6,
        Failed = 7,
        Returned = 8
    }
} 