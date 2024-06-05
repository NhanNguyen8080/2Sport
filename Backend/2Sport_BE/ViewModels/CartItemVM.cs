<<<<<<< HEAD
﻿namespace _2Sport_BE.ViewModels
{
    public class CartItemVM
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }
    }
=======
﻿using _2Sport_BE.Repository.Models;

namespace _2Sport_BE.ViewModels
{
    public class CartItemDTO
    {
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
    public class CartItemVM : CartItemDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CartItemCM : CartItemDTO
    {
    }

    public class CartItemUM : CartItemDTO
    {
    }


>>>>>>> 43cc858d1ff4be91d1343020888a25ced5919028
}
