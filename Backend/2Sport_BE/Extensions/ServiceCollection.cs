using _2Sport_BE.Repository.Interfaces;
using _2Sport_BE.Repository.Implements;
using _2Sport_BE.Repository.Models;
using Microsoft.EntityFrameworkCore;
using _2Sport_BE.Infrastructure.Services;
using _2Sport_BE.API.Services;
using Microsoft.IdentityModel.Tokens;
using _2Sport_BE.Service.Services;
using _2Sport_BE.Services;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace _2Sport_BE.Extensions
{
    public static class ServiceCollection
    {
        public static void Register (this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<TwoSportDBContext>(options => options
            .UseSqlServer(GetConnectionStrings()));
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            //Mail service
            
        }

        private static string GetConnectionStrings()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var strConn = config["ConnectionStrings:DefaultConnection"];
            return strConn; 
        }
    }
}
