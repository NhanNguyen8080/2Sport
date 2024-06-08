namespace _2Sport_BE.ViewModels
{
    public class OrderDTO
    {
        public decimal? TransportFee { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public List<OrderDetailRequest> OrderDetails { get; set; }
    }
    public class OrderCM : OrderDTO
    {
        public int? ShipmentDetailId { get; set; }
    }
    public class OrderUM : OrderDTO
    {
    }
    public class OrderVM : OrderDTO
    {
        public int? Status { get; set; }
        public decimal? IntoMoney { get; set; }
        public string? PaymentMethod { get; set; }
        public int? ShipmentDetailId { get; set; }
        public string? PaymentLink { get; set; }
    }
    public class OrderDetailRequest
    {
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
    }
    public class CancelPaymentRequest
    {
        public int OrderId { get; set; }
        public string Reason { get; set; }
    }
}
