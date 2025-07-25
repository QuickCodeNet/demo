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
    public class PaymentMonthsGetCommonExpensesForPaymentMonthsQuery : IRequest<Response<List<PaymentMonthsGetCommonExpensesForPaymentMonthsResponseDto>>>
    {
        public int PaymentMonthsId { get; set; }

        public PaymentMonthsGetCommonExpensesForPaymentMonthsQuery(int paymentMonthsId)
        {
            this.PaymentMonthsId = paymentMonthsId;
        }

        public class PaymentMonthsGetCommonExpensesForPaymentMonthsHandler : IRequestHandler<PaymentMonthsGetCommonExpensesForPaymentMonthsQuery, Response<List<PaymentMonthsGetCommonExpensesForPaymentMonthsResponseDto>>>
        {
            private readonly ILogger<PaymentMonthsGetCommonExpensesForPaymentMonthsHandler> _logger;
            private readonly IPaymentMonthsRepository _repository;
            public PaymentMonthsGetCommonExpensesForPaymentMonthsHandler(ILogger<PaymentMonthsGetCommonExpensesForPaymentMonthsHandler> logger, IPaymentMonthsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentMonthsGetCommonExpensesForPaymentMonthsResponseDto>>> Handle(PaymentMonthsGetCommonExpensesForPaymentMonthsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentMonthsGetCommonExpensesForPaymentMonthsAsync(request.PaymentMonthsId);
                return returnValue.ToResponse();
            }
        }
    }
}