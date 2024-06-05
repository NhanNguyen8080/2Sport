using _2Sport_BE.Repository.Models;
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
            #region ShipmentDetail
            CreateMap<ShipmentDetail, ShipmentDetailVM>();
            CreateMap<ShipmentDetail, ShipmentDetailCM>();
            CreateMap<ShipmentDetail, ShipmentDetailUM>();
            CreateMap<ShipmentDetailUM, ShipmentDetail>();
            #endregion
            #region PaymentMethod
            CreateMap<PaymentMethod, PaymentMethodCM>();
            CreateMap<PaymentMethod, PaymentMethodVM>();
            CreateMap<PaymentMethod, PaymentMethodUM>();
            CreateMap<PaymentMethodUM, PaymentMethod>();
            #endregion
            #region Order
            CreateMap<Order, OrderCM>();
            CreateMap<Order, OrderVM>();
            CreateMap<Order, OrderUM>();
            CreateMap<OrderUM, Order>();
            #endregion
            #region Category
            CreateMap<Category, CategoryVM>();
            CreateMap<Category, CategoryCM>();
            CreateMap<Category, CategoryUM>();
            CreateMap<CategoryUM, Category>();
            #endregion

            #region Product
            CreateMap<Product, ProductVM>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName));
            CreateMap<Product, ProductCM>();
            CreateMap<Product, ProductUM>();
            CreateMap<ProductUM, Product>();
            CreateMap<ProductCM, Product>();
            #endregion


        }


    }
}
