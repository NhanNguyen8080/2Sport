﻿using _2Sport_BE.Repository.Models;
using _2Sport_BE.ViewModels;
using AutoMapper;

namespace _2Sport_BE.Helpers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            #region User
            CreateMap<User, UserVM>()
                .ForMember(_dest => _dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
            CreateMap<UserCM, User>();
            CreateMap<UserUM, User>();
            #endregion

            #region Sport
            CreateMap<Sport, SportVM>();
            CreateMap<Sport, SportCM>();
            CreateMap<Sport, SportUM>();
            CreateMap<SportUM, Sport>();
            #endregion

            #region Category
            CreateMap<Category, CategoryVM>();
            CreateMap<Category, CategoryCM>();
            CreateMap<Category, CategoryUM>();
            CreateMap<CategoryUM, Category>();
            #endregion

            #region Product
            CreateMap<Product, ProductVM>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName))
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
				.ForMember(dest => dest.SportName, opt => opt.MapFrom(src => src.Sport.Name))
				.ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Count));
            CreateMap<Product, ProductCM>();
            CreateMap<Product, ProductUM>();
            CreateMap<ProductUM, Product>();
            CreateMap<ProductCM, Product>();
            #endregion

            #region CartItem
            CreateMap<CartItem, CartItemVM>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));
            CreateMap<CartItem, CartItemCM>().ReverseMap();
            CreateMap<CartItem, CartItemUM>().ReverseMap();

            #endregion

        }


    }
}
