using DeliveryService.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using DeliveryService.Domain.Entities;
using AutoMapper;
using DeliveryService.Domain;
using DeliveryService.Services.DTO;

namespace DeliveryService.Services.services
{
    internal class OrderService: IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавляет новый заказ в базу данных.
        /// </summary>
        /// <param name="dto">Входной объект передачи данных заказа, содержащий подробности заказа.</param>
        /// <returns>Идентификатор добавленного заказа.</returns>
        public async Task<int> AddNewOrder(InputOrderDto dto)
        {
            // Отображение входного DTO на новый объект заказа
            var order = _mapper.Map<Order>(dto);

            // Добавление нового заказа в репозиторий
            _unitOfWork.repository<Order>().Add(order);

            // Сохранение изменений в базе данных
            await _unitOfWork.Complete();

            // Возврат идентификатора добавленного заказа
            return order.Id;
        }

        /// <summary>
        /// Асинхронно получает список всех заказов.
        /// </summary>
        /// <returns>Список всех заказов в формате <see cref="OrderDto"/>.</returns>
        /// <remarks>
        /// Этот метод асинхронно запрашивает все заказы из базы данных и преобразует их в DTO объекты.
        /// </remarks>
        public async Task<bool> RemoveOrder(int OrederId)
        {
           
            var models = await _unitOfWork.repository<Order>().Get(m=>m.Id == OrederId);
            if(models.Any())
            {
              _unitOfWork.repository<Order>().DeleteAsync(models.First());
              await _unitOfWork.Complete();

                return true;
            }
            return false;
           
        }

        /// <summary>
        /// Получить все заказы
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrderDto>> GetAllOrders()
        {
            var models = await _unitOfWork.repository<Order>().GetAllAsync();
            return _mapper.Map<List<OrderDto>>(models);
        }

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
        public async Task<List<OrderDto>> FilterOrders(string _cityDistrict, DateTime _firstDeliveryDateTime)
        {
            var models = await _unitOfWork.repository<Order>().Get(m => m.Region == _cityDistrict &&
                                                     m.DeliveryTime >= _firstDeliveryDateTime &&
                                                     m.DeliveryTime <= _firstDeliveryDateTime.AddMinutes(30));          
            return _mapper.Map<List<OrderDto>>(models);
        }

        /// <summary>
        /// for Validation
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <returns></returns>
        public async Task<bool> IsOrderNumberExist(string OrderNumber)
        {

            var exists = await _unitOfWork.repository<Order>().Get(o => o.OrderNumber == OrderNumber);
            return exists.Any();
        }

    }
}
