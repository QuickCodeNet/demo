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
    public class PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesQuery : IRequest<Response<List<PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesResponseDto>>>
    {
        public int PaymentTypesId { get; set; }

        public PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesQuery(int paymentTypesId)
        {
            this.PaymentTypesId = paymentTypesId;
        }

        public class PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesHandler : IRequestHandler<PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesQuery, Response<List<PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesResponseDto>>>
        {
            private readonly ILogger<PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesHandler> _logger;
            private readonly IPaymentTypesRepository _repository;
            public PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesHandler(ILogger<PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesHandler> logger, IPaymentTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesResponseDto>>> Handle(PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesAsync(request.PaymentTypesId);
                return returnValue.ToResponse();
            }
        }
    }
}