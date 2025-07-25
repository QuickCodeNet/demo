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
    public class PaymentMonthsTotalItemCountQuery : IRequest<Response<int>>
    {
        public PaymentMonthsTotalItemCountQuery()
        {
        }

        public class PaymentMonthsTotalItemCountHandler : IRequestHandler<PaymentMonthsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<PaymentMonthsTotalItemCountHandler> _logger;
            private readonly IPaymentMonthsRepository _repository;
            public PaymentMonthsTotalItemCountHandler(ILogger<PaymentMonthsTotalItemCountHandler> logger, IPaymentMonthsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(PaymentMonthsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}