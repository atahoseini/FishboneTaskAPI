using AutoMapper;
using Fishbone.Application.Interfaces;
using Fishbone.Application.Services;
using Fishbone.Core.Dto;
using Fishbone.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Fishbone.Application
{
    public static class DIRegister
    {
        public static IServiceCollection AddApplicaitionServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IProductServices, ProducServices>();
            return services;
        }
    }



    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Order, OrderModifiedDto>().ReverseMap();
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User
                {
                    Id = src.User.Id,
                    UserName = src.User.UserName,
                    FirstName = src.User.FirstName,
                    LastName = src.User.LastName,
                    Password = "*",
                    PasswordSalt = "*",
                }))
                .ReverseMap();
            CreateMap<OrderDto, OrderModifiedDto>()
                .ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            //CreateMap<AddRefreshTokenNotification, UserRefreshToken>()
            //.ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now))
            //.ForMember(dest => dest.IsValid, opt => opt.MapFrom(src => true));
        }
    }
}
