using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Services.DTO
{
    public class IPAdressLogDto
    {
        public int Id { get; set; }

        public string Ip { get; set; }
        /// <summary>
        /// Тип операции с параметрами
        /// </summary>
        public string Operation { get; set; }
        public DateTime Date { get; set; }

    }
}
