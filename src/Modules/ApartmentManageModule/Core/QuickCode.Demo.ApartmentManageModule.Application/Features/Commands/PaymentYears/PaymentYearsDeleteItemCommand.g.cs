using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Features
{
    public class PaymentYearsDeleteItemCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public PaymentYearsDeleteItemCommand(int id)
        {
            this.Id = id;
        }

        public class PaymentYearsDeleteItemHandler : IRequestHandler<PaymentYearsDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<PaymentYearsDeleteItemHandler> _logger;
            private readonly IPaymentYearsRepository _repository;
            public PaymentYearsDeleteItemHandler(ILogger<PaymentYearsDeleteItemHandler> logger, IPaymentYearsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PaymentYearsDeleteItemCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Id);
                if (deleteItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}