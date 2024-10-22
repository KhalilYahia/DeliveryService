using DeliveryService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Domain.Entities
{
    public class CustomUser:IdentityUser
    {
        public float money { get; set; }
        public string? DisplayName { get; set; }

       
    }
}
