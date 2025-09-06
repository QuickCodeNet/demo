using System;
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
    public class PaymentMonthsGetAllMonthsQuery : IRequest<Response<List<PaymentMonthsGetAllMonthsResponseDto>>>
    {
        public PaymentMonthsGetAllMonthsQuery()
        {
        }

        public class PaymentMonthsGetAllMonthsHandler : IRequestHandler<PaymentMonthsGetAllMonthsQuery, Response<List<PaymentMonthsGetAllMonthsResponseDto>>>
        {
            private readonly ILogger<PaymentMonthsGetAllMonthsHandler> _logger;
            private readonly IPaymentMonthsRepository _repository;
            public PaymentMonthsGetAllMonthsHandler(ILogger<PaymentMonthsGetAllMonthsHandler> logger, IPaymentMonthsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentMonthsGetAllMonthsResponseDto>>> Handle(PaymentMonthsGetAllMonthsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentMonthsGetAllMonthsAsync();
                return returnValue.ToResponse();
            }
        }
    }
}