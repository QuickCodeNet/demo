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
    public class PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesDetailsQuery : IRequest<Response<PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesResponseDto>>
    {
        public int PaymentTypesId { get; set; }
        public int FlatExpenseInstallmentsId { get; set; }

        public PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesDetailsQuery(int paymentTypesId, int flatExpenseInstallmentsId)
        {
            this.PaymentTypesId = paymentTypesId;
            this.FlatExpenseInstallmentsId = flatExpenseInstallmentsId;
        }

        public class PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesDetailsHandler : IRequestHandler<PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesDetailsQuery, Response<PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesResponseDto>>
        {
            private readonly ILogger<PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesDetailsHandler> _logger;
            private readonly IPaymentTypesRepository _repository;
            public PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesDetailsHandler(ILogger<PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesDetailsHandler> logger, IPaymentTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesResponseDto>> Handle(PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentTypesGetFlatExpenseInstallmentsForPaymentTypesDetailsAsync(request.PaymentTypesId, request.FlatExpenseInstallmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}