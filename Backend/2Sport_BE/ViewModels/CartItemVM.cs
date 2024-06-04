namespace _2Sport_BE.ViewModels
{
    public class CartItemVM
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
