
using Microsoft.Extensions.DependencyInjection;

using System.Data;
using DeliveryService.Domain;
using DeliveryService.Data;
using DeliveryService.Services.Iservices;
using DeliveryService.Services.services;
using AutoMapper;
using DeliveryService.Services;
using FluentValidation;
using DeliveryService.Domain.Entities;
using DeleveryService.Services.DTO.Validators;
using DeliveryService.Services.DTO;



namespace Services.DependenyInjection
{
    public static class IServiceDependenyInjection
    {
        public static void SetDependencies(this IServiceCollection serviceCollection/*, IConfigurationRoot configuration*/)
        {
            #region important region

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ITokenService, TokenService>();

            serviceCollection.AddAutoMapper(typeof(MappingProfiles));

            #endregion

            
           
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IOrderService, OrderService>();
            serviceCollection.AddScoped<IIPAdressLogService, IPAdressLogService>();

            // validation area
            serviceCollection.AddScoped<IValidator<InputOrderDto>, InputOrderDtoValidator>();
            //
        }


    }
}
