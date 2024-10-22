using AutoMapper;
using DeliveryService.Services.DTO;
using DeliveryService.Services.Iservices;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DeliveryService.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для скачивания лог-файлов. 
    /// Этот метод возвращает лог-файл за текущий день, если он существует.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {

        private readonly IIPAdressLogService _IPAdressLogService;
        public LogController( IIPAdressLogService iPAdressLogService)
        {
            this._IPAdressLogService = iPAdressLogService;
        }

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
        [HttpGet("CountIpRequestsAsync")]
        public async Task<Dictionary<string, int>> CountIpRequestsAsync(DateTime startTime, DateTime endTime)
        {
            var res_ = await _IPAdressLogService.CountIpRequestsAsync(startTime, endTime); // save IP Address in database

            return res_;
        }
       

        /// <summary>
        /// Эндпоинт для скачивания лог-файла.
        /// Лог-файл за текущий день возвращается в виде plain text.
        /// Если файл не найден или его невозможно прочитать, возвращается ошибка.
        /// </summary>
        /// <returns>
        /// Возвращает файл с логами за текущий день в формате "text/plain".
        /// Если файл не найден, возвращается статус 404.
        /// В случае ошибки при чтении файла возвращается статус 500 с сообщением об ошибке.
        /// </returns>
        [HttpGet("DownloadLog")]
        public IActionResult DownloadLog(DateTime date)
        {
            // Определение пути к лог-файлу за текущий день
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", $"log-{date:yyyyMMdd}.txt");

            if (!System.IO.File.Exists(logFilePath))
            {
                return NotFound("Лог-файл не найден"); // Возвращаем 404, если файл не найден
            }

            // Open the file with read access and allow sharing with other processes
            try
            {
                using (var stream = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    return File(bytes, "text/plain", $"log-{DateTime.UtcNow:yyyy-MM-dd}.txt");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при чтении лог-файла: {ex.Message}");
            }
        }
    }
}
