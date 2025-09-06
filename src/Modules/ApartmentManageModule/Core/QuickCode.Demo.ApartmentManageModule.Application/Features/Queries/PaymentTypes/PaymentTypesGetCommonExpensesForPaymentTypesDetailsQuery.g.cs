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
    public class PaymentTypesGetCommonExpensesForPaymentTypesDetailsQuery : IRequest<Response<PaymentTypesGetCommonExpensesForPaymentTypesResponseDto>>
    {
        public int PaymentTypesId { get; set; }
        public int CommonExpensesId { get; set; }

        public PaymentTypesGetCommonExpensesForPaymentTypesDetailsQuery(int paymentTypesId, int commonExpensesId)
        {
            this.PaymentTypesId = paymentTypesId;
            this.CommonExpensesId = commonExpensesId;
        }

        public class PaymentTypesGetCommonExpensesForPaymentTypesDetailsHandler : IRequestHandler<PaymentTypesGetCommonExpensesForPaymentTypesDetailsQuery, Response<PaymentTypesGetCommonExpensesForPaymentTypesResponseDto>>
        {
            private readonly ILogger<PaymentTypesGetCommonExpensesForPaymentTypesDetailsHandler> _logger;
            private readonly IPaymentTypesRepository _repository;
            public PaymentTypesGetCommonExpensesForPaymentTypesDetailsHandler(ILogger<PaymentTypesGetCommonExpensesForPaymentTypesDetailsHandler> logger, IPaymentTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentTypesGetCommonExpensesForPaymentTypesResponseDto>> Handle(PaymentTypesGetCommonExpensesForPaymentTypesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentTypesGetCommonExpensesForPaymentTypesDetailsAsync(request.PaymentTypesId, request.CommonExpensesId);
                return returnValue.ToResponse();
            }
        }
    }
}