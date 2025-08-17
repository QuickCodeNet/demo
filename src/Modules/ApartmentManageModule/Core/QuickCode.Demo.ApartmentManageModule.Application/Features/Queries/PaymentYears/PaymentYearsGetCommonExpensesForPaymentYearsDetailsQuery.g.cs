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
    public class PaymentYearsGetCommonExpensesForPaymentYearsDetailsQuery : IRequest<Response<PaymentYearsGetCommonExpensesForPaymentYearsResponseDto>>
    {
        public int PaymentYearsId { get; set; }
        public int CommonExpensesId { get; set; }

        public PaymentYearsGetCommonExpensesForPaymentYearsDetailsQuery(int paymentYearsId, int commonExpensesId)
        {
            this.PaymentYearsId = paymentYearsId;
            this.CommonExpensesId = commonExpensesId;
        }

        public class PaymentYearsGetCommonExpensesForPaymentYearsDetailsHandler : IRequestHandler<PaymentYearsGetCommonExpensesForPaymentYearsDetailsQuery, Response<PaymentYearsGetCommonExpensesForPaymentYearsResponseDto>>
        {
            private readonly ILogger<PaymentYearsGetCommonExpensesForPaymentYearsDetailsHandler> _logger;
            private readonly IPaymentYearsRepository _repository;
            public PaymentYearsGetCommonExpensesForPaymentYearsDetailsHandler(ILogger<PaymentYearsGetCommonExpensesForPaymentYearsDetailsHandler> logger, IPaymentYearsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentYearsGetCommonExpensesForPaymentYearsResponseDto>> Handle(PaymentYearsGetCommonExpensesForPaymentYearsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentYearsGetCommonExpensesForPaymentYearsDetailsAsync(request.PaymentYearsId, request.CommonExpensesId);
                return returnValue.ToResponse();
            }
        }
    }
}