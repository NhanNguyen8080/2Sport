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
        }


    }
}
