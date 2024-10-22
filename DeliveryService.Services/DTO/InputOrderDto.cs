using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Services.DTO
{
    /// <summary>
    /// Это для дополнения
    /// </summary>
    public class InputOrderDto
    {
        /// <summary>
        /// Id заказа
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Номер заказа
        /// </summary>
        public string OrderNumber { get; set; }
        /// <summary>
        /// Вес заказа в килограммах;
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// Район заказа Например, Лефортва
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// Местоположение на карте Например 55.757257, 37.712659
        /// </summary>
        public string Coordinates { get; set; }
        /// <summary>
        /// Время доставки заказа
        /// </summary>
        public DateTime DeliveryTime { get; set; }

    }
}
