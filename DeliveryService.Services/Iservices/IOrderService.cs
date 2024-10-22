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
    public interface IOrderService
    {
        /// <summary>
        /// Добавляет новый заказ в базу данных.
        /// </summary>
        /// <param name="dto">Входной объект передачи данных заказа, содержащий подробности заказа.</param>
        /// <returns>Идентификатор добавленного заказа.</returns>
        Task<int> AddNewOrder(InputOrderDto dto);

        /// <summary>
        /// Асинхронно получает список всех заказов.
        /// </summary>
        /// <returns>Список всех заказов в формате <see cref="OrderDto"/>.</returns>
        /// <remarks>
        /// Этот метод асинхронно запрашивает все заказы из базы данных и преобразует их в DTO объекты.
        /// </remarks>
        Task<bool> RemoveOrder(int OrederId);

        /// <summary>
        /// Получить все заказы
        /// </summary>
        /// <returns></returns>
        Task<List<OrderDto>> GetAllOrders();

        /// <summary>
        /// Фильтрует заказы по указанному району города и интервалу времени доставки.
        /// </summary>
        /// <param name="_cityDistrict">Название района города, по которому производится фильтрация.</param>
        /// <param name="_firstDeliveryDateTime">Время начала интервала доставки (интервал составляет 30 минут).</param>
        /// <returns>Список заказов, соответствующих указанным критериям фильтрации, в формате <see cref="OrderDto"/>.</returns>
        /// <remarks>
        /// Этот метод асинхронно запрашивает заказы из базы данных, проверяя, что район совпадает с указанным,
        /// а время доставки попадает в интервал от <paramref name="_firstDeliveryDateTime"/> до <paramref name="_firstDeliveryDateTime"/> + 30 минут.
        /// Возвращает список заказов, преобразованный в DTO объекты.
        /// </remarks>
        Task<List<OrderDto>> FilterOrders(string _cityDistrict, DateTime _firstDeliveryDateTime);

        /// <summary>
        /// for Validation
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <returns></returns>
        Task<bool> IsOrderNumberExist(string OrderNumber);
    }
}
