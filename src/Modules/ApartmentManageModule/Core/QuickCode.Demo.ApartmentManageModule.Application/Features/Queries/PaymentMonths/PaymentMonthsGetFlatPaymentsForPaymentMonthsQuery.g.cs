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
    public class PaymentMonthsGetFlatPaymentsForPaymentMonthsQuery : IRequest<Response<List<PaymentMonthsGetFlatPaymentsForPaymentMonthsResponseDto>>>
    {
        public int PaymentMonthsId { get; set; }

        public PaymentMonthsGetFlatPaymentsForPaymentMonthsQuery(int paymentMonthsId)
        {
            this.PaymentMonthsId = paymentMonthsId;
        }

        public class PaymentMonthsGetFlatPaymentsForPaymentMonthsHandler : IRequestHandler<PaymentMonthsGetFlatPaymentsForPaymentMonthsQuery, Response<List<PaymentMonthsGetFlatPaymentsForPaymentMonthsResponseDto>>>
        {
            private readonly ILogger<PaymentMonthsGetFlatPaymentsForPaymentMonthsHandler> _logger;
            private readonly IPaymentMonthsRepository _repository;
            public PaymentMonthsGetFlatPaymentsForPaymentMonthsHandler(ILogger<PaymentMonthsGetFlatPaymentsForPaymentMonthsHandler> logger, IPaymentMonthsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentMonthsGetFlatPaymentsForPaymentMonthsResponseDto>>> Handle(PaymentMonthsGetFlatPaymentsForPaymentMonthsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentMonthsGetFlatPaymentsForPaymentMonthsAsync(request.PaymentMonthsId);
                return returnValue.ToResponse();
            }
        }
    }
}