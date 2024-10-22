using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common
{
    public static class Utils
    {
        public static DateTime ServerNow
        {
            get
            {
                DateTime date1 = DateTime.UtcNow;

                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Syria Standard Time");

                DateTime date2 = TimeZoneInfo.ConvertTime(date1, tz);
                // return DateTime.Now.AddHours(11);
                return date2;
            }
        }
        public static string API_PATH = "http://localhost:40008/";

        public static string PhysicalImageCategory = "/AllImages/CategoryImg/";
       // public static string ImageCategoryURL = API_PATH + "/AllImages/CategoryImg/";

        public static string PhysicalImageAdvertisment = "/AllImages/Homes/";
       // public static string ImageAdvertismentURL = API_PATH + "/AllImages/Homes/";

        public static string PhysicalLLAMA_Model = "/LLAMA_Model/";

        public static string ImageDefaultName = "index.png";

        public enum DirectionType
        {
            In,
            Out
        }
    }

   
    public enum LanguageHelper { RUSSIAN = 1, ENGLISH = 2 };
    public static class Roles
    {
        public static string DeveloperRole = "Developer";
        public static string AdminRole = "Admin";
        public static string AdvertiserUserRole = "Advertiser";
        public static string NormalUserRole = "NormalUser";
        public static string BannedUserRole = "BannedUser";

    }
}
