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
    public class PaymentTypesGetExpenseInstallmentsForPaymentTypesQuery : IRequest<Response<List<PaymentTypesGetExpenseInstallmentsForPaymentTypesResponseDto>>>
    {
        public int PaymentTypesId { get; set; }

        public PaymentTypesGetExpenseInstallmentsForPaymentTypesQuery(int paymentTypesId)
        {
            this.PaymentTypesId = paymentTypesId;
        }

        public class PaymentTypesGetExpenseInstallmentsForPaymentTypesHandler : IRequestHandler<PaymentTypesGetExpenseInstallmentsForPaymentTypesQuery, Response<List<PaymentTypesGetExpenseInstallmentsForPaymentTypesResponseDto>>>
        {
            private readonly ILogger<PaymentTypesGetExpenseInstallmentsForPaymentTypesHandler> _logger;
            private readonly IPaymentTypesRepository _repository;
            public PaymentTypesGetExpenseInstallmentsForPaymentTypesHandler(ILogger<PaymentTypesGetExpenseInstallmentsForPaymentTypesHandler> logger, IPaymentTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentTypesGetExpenseInstallmentsForPaymentTypesResponseDto>>> Handle(PaymentTypesGetExpenseInstallmentsForPaymentTypesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentTypesGetExpenseInstallmentsForPaymentTypesAsync(request.PaymentTypesId);
                return returnValue.ToResponse();
            }
        }
    }
}