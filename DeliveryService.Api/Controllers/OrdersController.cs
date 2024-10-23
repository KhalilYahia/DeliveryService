
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
            await _IPAdressLogService.Add(ipAddress,"Add new order function"); // save IP Address in database

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
            await _IPAdressLogService.Add(ipAddress,"Get all orders function"); // save IP Address in database

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
            await _IPAdressLogService.Add(ipAddress, "filter function with parameters: _citydistrict: "+ _citydistrict + ", _firstDeliveryDateTime: "+ _firstDeliveryDateTime); // save IP Address in database

            #region Validation
            if (_citydistrict.IsNullOrEmpty()) 
            {
                return BadRequest("_citydistrict can't be null or empty"); // Return validation errors
            }
            if(_firstDeliveryDateTime< new DateTime(2020,1,1))
            {
                return BadRequest("_firstDeliveryDateTime field is invalid or the date is too old."); // Return validation errors
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


        /// <summary>
        /// Возвращаем результат в файл .txt
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
        [HttpGet("FilterOrders_file")]
        public async Task<IActionResult> FilterOrders_file(string _citydistrict, DateTime _firstDeliveryDateTime)
        {
            // Получение IP-адреса клиента из HttpContext
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            await _IPAdressLogService.Add(ipAddress, "write the result of filter function to file. the parameters are: _citydistrict: " + _citydistrict + ", _firstDeliveryDateTime: " + _firstDeliveryDateTime); // save IP Address in database

            #region Validation
            if (_citydistrict.IsNullOrEmpty())
            {
                return BadRequest("_citydistrict can't be null or empty"); // Return validation errors
            }
            if (_firstDeliveryDateTime < new DateTime(2020, 1, 1))
            {
                return BadRequest("_firstDeliveryDateTime field is invalid or the date is too old."); // Return validation errors
            }
            #endregion

            var res_ = await _IOrderService.FilterOrders(_citydistrict, _firstDeliveryDateTime); // get data

            // to log operation
            DateTime endTime = _firstDeliveryDateTime.AddMinutes(30);
            Log.Information("Filtering orders for district {_citydistrict} between {_firstDeliveryDateTime} and {endTime}", _citydistrict, _firstDeliveryDateTime, endTime);


            // File path for saving results
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "OrderResults.txt");

            try
            {
                // Write results to file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Filter Orders Results:");
                    writer.WriteLine($"City District: {_citydistrict}");
                    writer.WriteLine($"Filtered between: {_firstDeliveryDateTime} and {endTime}");
                    writer.WriteLine("Order List:");

                    if (res_.Any())
                    {
                        foreach (var order in res_)
                        {
                            writer.WriteLine($"Order ID: {order.Id}, OrderNumber: {order.OrderNumber},Weight:{order.Weight}, Region: {order.Region}, Date: {order.DeliveryTime}");
                        }
                    }
                    else
                    {
                        writer.WriteLine("No results found.");
                    }
                }

                // Return the file path as a response
                return Ok($"Results saved in file: {filePath}");
            }
            catch (Exception ex)
            {
                Log.Error("Error writing file: {Message}", ex.Message);
                return StatusCode(500, "Error saving results to file.");
            }

        }

    }
   
}
