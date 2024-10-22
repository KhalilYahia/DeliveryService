using DeliveryService.Common;
using DeliveryService.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryService.Domain.Entities;

namespace DeliveryService.Services.Iservices
{
    public interface IUserService
    {
       Task<List<UserDto>> GetAllUsers_forAdmin();

       Task<UserDto> GetUserById_forAdmin(string userId);


       Task<List<RoleDto>> GetAllRoles_forAdmin();


    }
}
