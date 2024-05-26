using System;
using System.Collections.Generic;

namespace _2Sport_BE.Repository.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Logo { get; set; }
        public int? Quantity { get; set; }
        public bool? Status { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
