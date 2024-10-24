﻿using DeliveryService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Services.Iservices
{
    public interface ITokenService
    {
        string CreateToken(CustomUser appUser, string role);
    }
}
