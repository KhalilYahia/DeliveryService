using DeliveryService.Domain.Entities;
using DeliveryService.Services.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Services.Iservices
{
    public interface IIPAdressLogService
    {
        Task<int> Add(string Ip);


        Task<bool> Remove(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<IPAdressLogDto>> GetAllIPAdressLog();


        /// <summary>
        /// Асинхронный метод для подсчета количества запросов с каждого IP-адреса
        /// за указанный временной интервал.
        /// </summary>
        /// <param name="startTime">Начальное время для фильтрации логов IP-адресов.</param>
        /// <param name="endTime">Конечное время для фильтрации логов IP-адресов.</param>
        /// <returns>
        /// Возвращает словарь, где ключом является IP-адрес, а значением - количество запросов
        /// с этого IP-адреса в указанный временной интервал.
        /// </returns>
        Task<Dictionary<string, int>> CountIpRequestsAsync(DateTime startTime, DateTime endTime);
    }
}
