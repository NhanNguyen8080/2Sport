﻿using _2Sport_BE.Repository.Models;

namespace _2Sport_BE.ViewModels
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal? ListedPrice { get; set; }
        public decimal? Price { get; set; }
        public decimal Size { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public string Color { get; set; }
        public string Offers { get; set; }
        public string MainImageName { get; set; }
        public string MainImagePath { get; set; }
    }
    public class ProductVM : ProductDTO
    {
        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public ICollection<ImagesVideo> ImagesVideos { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }

    public class ProductCM : ProductDTO
    {
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? SportId { get; set; }

    }

    public class ProductUM : ProductDTO
    {
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? SportId { get; set; }

    }
}
