using DeliveryService.Domain.Entities;
using DeliveryService.Services.DTO;
using DeliveryService.Services.Iservices;
using DeliveryService.Services.services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleveryService.Services.DTO.Validators
{
    public class InputOrderDtoValidator : AbstractValidator<InputOrderDto>
    {
        public readonly IOrderService _OrderService;
        public InputOrderDtoValidator(IOrderService OrderService)
        {
            _OrderService = OrderService;

            RuleFor(x => x.Coordinates).NotEmpty().WithMessage("Требуются координаты")
                .NotNull().WithMessage("Требуются координаты")
                .Matches(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$")
                .WithMessage("Координаты должны быть в формате 'latitude, longitude'");

            RuleFor(x => x.Region).NotEmpty().WithMessage("район Требуется")
                .NotNull().WithMessage("район Требуется")
                .MaximumLength(50).WithMessage("максимальное количество символов - 50");
            
            RuleFor(x => x.DeliveryTime).Must(BeARecentDate).WithMessage("DeliveryTime не может быть слишком старым.");

            RuleFor(x=>x.OrderNumber).NotEmpty().WithMessage("Номер заказа обязателен")
                .NotNull().WithMessage("Номер заказа обязателен")
                .MaximumLength(50).WithMessage("максимальное количество символов - 50")
            .MustAsync(async (orderNumber, cancellation) => 
            {
                return !(await _OrderService.IsOrderNumberExist(orderNumber));
                
            }).WithMessage("Номер заказа уже существует в базе данных");
                
        }

        // Custom validator method to check if the date is not too old
        private bool BeARecentDate(DateTime deliveryTime)
        {
            // Define the threshold for how old the date can be, e.g., 1 year from the current date
            var oneYearAgo = DateTime.Now.AddYears(-1);
            return deliveryTime > oneYearAgo;
        }
    }
}
