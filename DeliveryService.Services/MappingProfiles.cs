using AutoMapper;
using AutoMapper.Features;
using DeliveryService.Common;
using DeliveryService.Domain.Entities;
using DeliveryService.Services.DTO;
using DeliveryService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Services
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Dto to Entity
            CreateMap<InputOrderDto, Order>();
           
            //
            #endregion

            //

            #region Entity To Dto 
           

            CreateMap<CustomUser, UserDto>();
            CreateMap<CustomRole, RoleDto>();
            CreateMap<Order, OrderDto>()
                .ForMember(m=>m.DeliveryTime, opt=>opt.MapFrom(src=>src.DeliveryTime.ToString("yyyy-MM-dd HH:mm:ss")));
           
            CreateMap<IPAdressLog, IPAdressLogDto>();
            //
            #endregion

        }

    }
}
