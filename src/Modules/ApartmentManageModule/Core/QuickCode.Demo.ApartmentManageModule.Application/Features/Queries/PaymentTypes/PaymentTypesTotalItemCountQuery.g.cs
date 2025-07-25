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
    public class PaymentTypesTotalItemCountQuery : IRequest<Response<int>>
    {
        public PaymentTypesTotalItemCountQuery()
        {
        }

        public class PaymentTypesTotalItemCountHandler : IRequestHandler<PaymentTypesTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<PaymentTypesTotalItemCountHandler> _logger;
            private readonly IPaymentTypesRepository _repository;
            public PaymentTypesTotalItemCountHandler(ILogger<PaymentTypesTotalItemCountHandler> logger, IPaymentTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(PaymentTypesTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}