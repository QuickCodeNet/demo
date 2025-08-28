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
    public class PaymentYearsGetCommonExpensesForPaymentYearsQuery : IRequest<Response<List<PaymentYearsGetCommonExpensesForPaymentYearsResponseDto>>>
    {
        public int PaymentYearsId { get; set; }

        public PaymentYearsGetCommonExpensesForPaymentYearsQuery(int paymentYearsId)
        {
            this.PaymentYearsId = paymentYearsId;
        }

        public class PaymentYearsGetCommonExpensesForPaymentYearsHandler : IRequestHandler<PaymentYearsGetCommonExpensesForPaymentYearsQuery, Response<List<PaymentYearsGetCommonExpensesForPaymentYearsResponseDto>>>
        {
            private readonly ILogger<PaymentYearsGetCommonExpensesForPaymentYearsHandler> _logger;
            private readonly IPaymentYearsRepository _repository;
            public PaymentYearsGetCommonExpensesForPaymentYearsHandler(ILogger<PaymentYearsGetCommonExpensesForPaymentYearsHandler> logger, IPaymentYearsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentYearsGetCommonExpensesForPaymentYearsResponseDto>>> Handle(PaymentYearsGetCommonExpensesForPaymentYearsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentYearsGetCommonExpensesForPaymentYearsAsync(request.PaymentYearsId);
                return returnValue.ToResponse();
            }
        }
    }
}