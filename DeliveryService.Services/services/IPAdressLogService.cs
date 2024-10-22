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
using DeliveryService.Common;

namespace DeliveryService.Services.services
{
    internal class IPAdressLogService : IIPAdressLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public IPAdressLogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Add(string Ip)
        {

            var model = new IPAdressLog
            {
                Ip = Ip,
                Date = Utils.ServerNow
            };
            _unitOfWork.repository<IPAdressLog>().Add(model);
            await _unitOfWork.Complete();

            return model.Id;
        }

        
        public async Task<bool> Remove(int Id)
        {
           
            var models = await _unitOfWork.repository<IPAdressLog>().Get(m=>m.Id == Id);
            if(models.Any())
            {
              _unitOfWork.repository<IPAdressLog>().DeleteAsync(models.First());
              await _unitOfWork.Complete();

                return true;
            }
            return false;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<IPAdressLogDto>> GetAllIPAdressLog()
        {
            var models = await _unitOfWork.repository<IPAdressLog>().GetAllAsync();
            return _mapper.Map<List<IPAdressLogDto>>(models);
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
        public async Task<Dictionary<string, int>> CountIpRequestsAsync(DateTime startTime, DateTime endTime)
        {
            var models = await _unitOfWork.repository<IPAdressLog>().GetAllAsync();
            var res_ = models
                .Where(entry => entry.Date >= startTime && entry.Date <= endTime)
                .GroupBy(entry => entry.Ip)
                .ToDictionary(group => group.Key, group => group.Count());

            return res_;
        }

    }
}
