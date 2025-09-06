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
    public class PaymentMonthsGetCommonExpensesForPaymentMonthsDetailsQuery : IRequest<Response<PaymentMonthsGetCommonExpensesForPaymentMonthsResponseDto>>
    {
        public int PaymentMonthsId { get; set; }
        public int CommonExpensesId { get; set; }

        public PaymentMonthsGetCommonExpensesForPaymentMonthsDetailsQuery(int paymentMonthsId, int commonExpensesId)
        {
            this.PaymentMonthsId = paymentMonthsId;
            this.CommonExpensesId = commonExpensesId;
        }

        public class PaymentMonthsGetCommonExpensesForPaymentMonthsDetailsHandler : IRequestHandler<PaymentMonthsGetCommonExpensesForPaymentMonthsDetailsQuery, Response<PaymentMonthsGetCommonExpensesForPaymentMonthsResponseDto>>
        {
            private readonly ILogger<PaymentMonthsGetCommonExpensesForPaymentMonthsDetailsHandler> _logger;
            private readonly IPaymentMonthsRepository _repository;
            public PaymentMonthsGetCommonExpensesForPaymentMonthsDetailsHandler(ILogger<PaymentMonthsGetCommonExpensesForPaymentMonthsDetailsHandler> logger, IPaymentMonthsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentMonthsGetCommonExpensesForPaymentMonthsResponseDto>> Handle(PaymentMonthsGetCommonExpensesForPaymentMonthsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentMonthsGetCommonExpensesForPaymentMonthsDetailsAsync(request.PaymentMonthsId, request.CommonExpensesId);
                return returnValue.ToResponse();
            }
        }
    }
}