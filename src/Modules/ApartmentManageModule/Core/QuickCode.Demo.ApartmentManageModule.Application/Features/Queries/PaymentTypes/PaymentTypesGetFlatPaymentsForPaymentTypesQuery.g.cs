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
    public class PaymentTypesGetFlatPaymentsForPaymentTypesQuery : IRequest<Response<List<PaymentTypesGetFlatPaymentsForPaymentTypesResponseDto>>>
    {
        public int PaymentTypesId { get; set; }

        public PaymentTypesGetFlatPaymentsForPaymentTypesQuery(int paymentTypesId)
        {
            this.PaymentTypesId = paymentTypesId;
        }

        public class PaymentTypesGetFlatPaymentsForPaymentTypesHandler : IRequestHandler<PaymentTypesGetFlatPaymentsForPaymentTypesQuery, Response<List<PaymentTypesGetFlatPaymentsForPaymentTypesResponseDto>>>
        {
            private readonly ILogger<PaymentTypesGetFlatPaymentsForPaymentTypesHandler> _logger;
            private readonly IPaymentTypesRepository _repository;
            public PaymentTypesGetFlatPaymentsForPaymentTypesHandler(ILogger<PaymentTypesGetFlatPaymentsForPaymentTypesHandler> logger, IPaymentTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentTypesGetFlatPaymentsForPaymentTypesResponseDto>>> Handle(PaymentTypesGetFlatPaymentsForPaymentTypesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentTypesGetFlatPaymentsForPaymentTypesAsync(request.PaymentTypesId);
                return returnValue.ToResponse();
            }
        }
    }
}