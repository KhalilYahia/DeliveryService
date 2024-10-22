using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Domain.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string RussianName { get; set; }
        public string EnglishName { get; set; }

       
    }
}
