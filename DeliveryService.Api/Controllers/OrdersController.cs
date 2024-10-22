
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using DeliveryService.Services.Iservices;
using DeliveryService.Services.DTO;
using Serilog;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;


namespace DeliveryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ApiBaseController
    {
       
        private readonly IOrderService _IOrderService;
        private readonly IIPAdressLogService _IPAdressLogService;
       
        private readonly IValidator<InputOrderDto> _validator;


        private readonly IMapper _mapper;
        public OrdersController(IOrderService _IOrderService, IValidator<InputOrderDto> validator, IIPAdressLogService iPAdressLogService)
        {
            this._IOrderService = _IOrderService;
            _validator = validator;
            this._IPAdressLogService = iPAdressLogService;
        }

        /// <summary>
        /// Добавляет новый заказ в базу данных.
        /// </summary>
        /// <param name="dto">Входной объект передачи данных заказа, содержащий подробности заказа.</param>
        /// <returns>Идентификатор добавленного заказа.</returns>
        [Authorize]        
        [HttpPost("AddNewOrder")]
        public async Task<ActionResult<int>> AddNewOrder(InputOrderDto order)
        {

            // Получение IP-адреса клиента из HttpContext
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            await _IPAdressLogService.Add(ipAddress); // save IP Address in database

            #region validation
            // Validate the order using FluentValidation
            var validationResult = await _validator.ValidateAsync(order);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // Return validation errors
            }
            #endregion

            var res = await _IOrderService.AddNewOrder(order);

            Log.Information("Order {OrderNumber} added", order.OrderNumber);

            return Ok(res);
        }


        /// <summary>
        /// Получить все заказы
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
        {
            Log.Information("Fetching all orders");
            // Получение IP-адреса клиента из HttpContext
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            await _IPAdressLogService.Add(ipAddress); // save IP Address in database

            return Ok(await _IOrderService.GetAllOrders());
        }


        /// <summary>
        /// Фильтрует заказы по указанному району города и интервалу времени доставки.
        /// </summary>
        /// <param name="_citydistrict">Название района города, по которому производится фильтрация.</param>
        /// <param name="_firstDeliveryDateTime">Время начала интервала доставки (интервал составляет 30 минут).</param>
        /// <returns>Список заказов, соответствующих указанным критериям фильтрации, в формате <see cref="OrderDto"/>.</returns>
        /// <remarks>
        /// Этот метод асинхронно запрашивает заказы из базы данных, проверяя, что район совпадает с указанным,
        /// а время доставки попадает в интервал от <paramref name="_firstDeliveryDateTime"/> до <paramref name="_firstDeliveryDateTime"/> + 30 минут.
        /// Возвращает список заказов, преобразованный в DTO объекты.
        /// </remarks>
        [HttpGet("FilterOrders")]
        public async Task<ActionResult<List<OrderDto>>> FilterOrders(string _citydistrict, DateTime _firstDeliveryDateTime)
        {
            // Получение IP-адреса клиента из HttpContext
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            await _IPAdressLogService.Add(ipAddress); // save IP Address in database

            #region Validation
            if (_citydistrict.IsNullOrEmpty()) 
            {
                return BadRequest("_citydistrict can't be null or empty"); // Return validation errors
            }
            if(_firstDeliveryDateTime< new DateTime(1000,1,1))
            {
                return BadRequest("_firstDeliveryDateTime isn't a vailed"); // Return validation errors
            }
            #endregion

            var res_ = await _IOrderService.FilterOrders(_citydistrict, _firstDeliveryDateTime); // get data

            // to log operation
            DateTime endTime = _firstDeliveryDateTime.AddMinutes(30);
            Log.Information("Filtering orders for district {_citydistrict} between {_firstDeliveryDateTime} and {endTime}", _citydistrict, _firstDeliveryDateTime, endTime);
           

            // return result
            if (res_.Any())
                return Ok(res_);
            else
                return NotFound("Нет результатов");

        }


    }
   
}
