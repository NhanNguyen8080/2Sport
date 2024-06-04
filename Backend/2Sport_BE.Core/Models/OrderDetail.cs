using System;
using System.Collections.Generic;

namespace _2Sport_BE.Repository.Models
{
    public partial class OrderDetail
    {
        public OrderDetail(int? productId, int? quantity, decimal? price)
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? OrderId { get; set; }
        public decimal? Price { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
