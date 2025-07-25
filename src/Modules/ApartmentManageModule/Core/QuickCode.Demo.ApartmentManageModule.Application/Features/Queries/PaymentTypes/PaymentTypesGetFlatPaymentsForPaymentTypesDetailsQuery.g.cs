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
    public class PaymentTypesGetFlatPaymentsForPaymentTypesDetailsQuery : IRequest<Response<PaymentTypesGetFlatPaymentsForPaymentTypesResponseDto>>
    {
        public int PaymentTypesId { get; set; }
        public int FlatPaymentsId { get; set; }

        public PaymentTypesGetFlatPaymentsForPaymentTypesDetailsQuery(int paymentTypesId, int flatPaymentsId)
        {
            this.PaymentTypesId = paymentTypesId;
            this.FlatPaymentsId = flatPaymentsId;
        }

        public class PaymentTypesGetFlatPaymentsForPaymentTypesDetailsHandler : IRequestHandler<PaymentTypesGetFlatPaymentsForPaymentTypesDetailsQuery, Response<PaymentTypesGetFlatPaymentsForPaymentTypesResponseDto>>
        {
            private readonly ILogger<PaymentTypesGetFlatPaymentsForPaymentTypesDetailsHandler> _logger;
            private readonly IPaymentTypesRepository _repository;
            public PaymentTypesGetFlatPaymentsForPaymentTypesDetailsHandler(ILogger<PaymentTypesGetFlatPaymentsForPaymentTypesDetailsHandler> logger, IPaymentTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentTypesGetFlatPaymentsForPaymentTypesResponseDto>> Handle(PaymentTypesGetFlatPaymentsForPaymentTypesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentTypesGetFlatPaymentsForPaymentTypesDetailsAsync(request.PaymentTypesId, request.FlatPaymentsId);
                return returnValue.ToResponse();
            }
        }
    }
}