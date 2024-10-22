using DeliveryService.Common;
using DeliveryService.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Services.Iservices
{
    public interface IEmailService
    {
        Task SendAsync(IdentityMessage message);

    }
}
