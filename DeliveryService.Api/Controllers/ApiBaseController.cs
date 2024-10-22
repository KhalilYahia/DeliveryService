using DeliveryService.Common;
using DeliveryService.Services.DTO;
using DeliveryService.Services.Iservices;
using LLama;
using LLama.Common;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Hosting;
using NuGet.Packaging.Signing;
using System.Net.NetworkInformation;
using System.Globalization;
using System.Security.Claims;



namespace DeliveryService.Controllers
{
  
    public class ApiBaseController : ControllerBase
    {
        protected string lang = "ru";

        public LanguageHelper CurrentLanguage
        {
            get
            {
                if (Request.Headers.ContentLanguage.ToString() == "en")
                    return LanguageHelper.ENGLISH;
                else
                    return LanguageHelper.RUSSIAN;
            }
        }
        public string GetuserId
        {
            get
            {
               return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            }
        }
        

    }
}
