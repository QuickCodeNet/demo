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
    public class PaymentTypesGetCommonExpensesForPaymentTypesQuery : IRequest<Response<List<PaymentTypesGetCommonExpensesForPaymentTypesResponseDto>>>
    {
        public int PaymentTypesId { get; set; }

        public PaymentTypesGetCommonExpensesForPaymentTypesQuery(int paymentTypesId)
        {
            this.PaymentTypesId = paymentTypesId;
        }

        public class PaymentTypesGetCommonExpensesForPaymentTypesHandler : IRequestHandler<PaymentTypesGetCommonExpensesForPaymentTypesQuery, Response<List<PaymentTypesGetCommonExpensesForPaymentTypesResponseDto>>>
        {
            private readonly ILogger<PaymentTypesGetCommonExpensesForPaymentTypesHandler> _logger;
            private readonly IPaymentTypesRepository _repository;
            public PaymentTypesGetCommonExpensesForPaymentTypesHandler(ILogger<PaymentTypesGetCommonExpensesForPaymentTypesHandler> logger, IPaymentTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentTypesGetCommonExpensesForPaymentTypesResponseDto>>> Handle(PaymentTypesGetCommonExpensesForPaymentTypesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentTypesGetCommonExpensesForPaymentTypesAsync(request.PaymentTypesId);
                return returnValue.ToResponse();
            }
        }
    }
}