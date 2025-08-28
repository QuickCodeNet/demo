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
    public class PaymentMonthsGetApartmentFeePlansForPaymentMonthsQuery : IRequest<Response<List<PaymentMonthsGetApartmentFeePlansForPaymentMonthsResponseDto>>>
    {
        public int PaymentMonthsId { get; set; }

        public PaymentMonthsGetApartmentFeePlansForPaymentMonthsQuery(int paymentMonthsId)
        {
            this.PaymentMonthsId = paymentMonthsId;
        }

        public class PaymentMonthsGetApartmentFeePlansForPaymentMonthsHandler : IRequestHandler<PaymentMonthsGetApartmentFeePlansForPaymentMonthsQuery, Response<List<PaymentMonthsGetApartmentFeePlansForPaymentMonthsResponseDto>>>
        {
            private readonly ILogger<PaymentMonthsGetApartmentFeePlansForPaymentMonthsHandler> _logger;
            private readonly IPaymentMonthsRepository _repository;
            public PaymentMonthsGetApartmentFeePlansForPaymentMonthsHandler(ILogger<PaymentMonthsGetApartmentFeePlansForPaymentMonthsHandler> logger, IPaymentMonthsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentMonthsGetApartmentFeePlansForPaymentMonthsResponseDto>>> Handle(PaymentMonthsGetApartmentFeePlansForPaymentMonthsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentMonthsGetApartmentFeePlansForPaymentMonthsAsync(request.PaymentMonthsId);
                return returnValue.ToResponse();
            }
        }
    }
}