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
    public class PaymentTypesGetActivePaymentTypesQuery : IRequest<Response<List<PaymentTypesGetActivePaymentTypesResponseDto>>>
    {
        public bool PaymentTypesIsActive { get; set; }

        public PaymentTypesGetActivePaymentTypesQuery(bool paymentTypesIsActive)
        {
            this.PaymentTypesIsActive = paymentTypesIsActive;
        }

        public class PaymentTypesGetActivePaymentTypesHandler : IRequestHandler<PaymentTypesGetActivePaymentTypesQuery, Response<List<PaymentTypesGetActivePaymentTypesResponseDto>>>
        {
            private readonly ILogger<PaymentTypesGetActivePaymentTypesHandler> _logger;
            private readonly IPaymentTypesRepository _repository;
            public PaymentTypesGetActivePaymentTypesHandler(ILogger<PaymentTypesGetActivePaymentTypesHandler> logger, IPaymentTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentTypesGetActivePaymentTypesResponseDto>>> Handle(PaymentTypesGetActivePaymentTypesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentTypesGetActivePaymentTypesAsync(request.PaymentTypesIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}