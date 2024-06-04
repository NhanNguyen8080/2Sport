namespace _2Sport_BE.ViewModels
{
    public class OrderDTO
    {
        public decimal? TotalPrice { get; set; }
        public decimal? TransportFee { get; set; }
        public decimal? IntoMoney { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? ShipmentDetailId { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public int? TransportUnitId { get; set; }
        public int? UserId { get; set; }
        public List<OrderDetailRequest> OrderDetails { get; set; }
    }
    public class OrderCM
    {
    }
    public class OrderUM
    {
    }
    public class OrderVM
    {
    }
    public class OrderDetailRequest
    {
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
