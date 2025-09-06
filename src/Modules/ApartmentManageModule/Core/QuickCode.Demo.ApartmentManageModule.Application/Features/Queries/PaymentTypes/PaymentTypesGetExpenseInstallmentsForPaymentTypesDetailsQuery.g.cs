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
    public class PaymentTypesGetExpenseInstallmentsForPaymentTypesDetailsQuery : IRequest<Response<PaymentTypesGetExpenseInstallmentsForPaymentTypesResponseDto>>
    {
        public int PaymentTypesId { get; set; }
        public int ExpenseInstallmentsId { get; set; }

        public PaymentTypesGetExpenseInstallmentsForPaymentTypesDetailsQuery(int paymentTypesId, int expenseInstallmentsId)
        {
            this.PaymentTypesId = paymentTypesId;
            this.ExpenseInstallmentsId = expenseInstallmentsId;
        }

        public class PaymentTypesGetExpenseInstallmentsForPaymentTypesDetailsHandler : IRequestHandler<PaymentTypesGetExpenseInstallmentsForPaymentTypesDetailsQuery, Response<PaymentTypesGetExpenseInstallmentsForPaymentTypesResponseDto>>
        {
            private readonly ILogger<PaymentTypesGetExpenseInstallmentsForPaymentTypesDetailsHandler> _logger;
            private readonly IPaymentTypesRepository _repository;
            public PaymentTypesGetExpenseInstallmentsForPaymentTypesDetailsHandler(ILogger<PaymentTypesGetExpenseInstallmentsForPaymentTypesDetailsHandler> logger, IPaymentTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentTypesGetExpenseInstallmentsForPaymentTypesResponseDto>> Handle(PaymentTypesGetExpenseInstallmentsForPaymentTypesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentTypesGetExpenseInstallmentsForPaymentTypesDetailsAsync(request.PaymentTypesId, request.ExpenseInstallmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}