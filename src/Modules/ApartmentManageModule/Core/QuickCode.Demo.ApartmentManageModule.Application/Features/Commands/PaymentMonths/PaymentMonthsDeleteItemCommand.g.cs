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
    public class PaymentMonthsDeleteItemCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public PaymentMonthsDeleteItemCommand(int id)
        {
            this.Id = id;
        }

        public class PaymentMonthsDeleteItemHandler : IRequestHandler<PaymentMonthsDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<PaymentMonthsDeleteItemHandler> _logger;
            private readonly IPaymentMonthsRepository _repository;
            public PaymentMonthsDeleteItemHandler(ILogger<PaymentMonthsDeleteItemHandler> logger, IPaymentMonthsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PaymentMonthsDeleteItemCommand request, CancellationToken cancellationToken)
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