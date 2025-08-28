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
    public class PaymentYearsGetFlatPaymentsForPaymentYearsDetailsQuery : IRequest<Response<PaymentYearsGetFlatPaymentsForPaymentYearsResponseDto>>
    {
        public int PaymentYearsId { get; set; }
        public int FlatPaymentsId { get; set; }

        public PaymentYearsGetFlatPaymentsForPaymentYearsDetailsQuery(int paymentYearsId, int flatPaymentsId)
        {
            this.PaymentYearsId = paymentYearsId;
            this.FlatPaymentsId = flatPaymentsId;
        }

        public class PaymentYearsGetFlatPaymentsForPaymentYearsDetailsHandler : IRequestHandler<PaymentYearsGetFlatPaymentsForPaymentYearsDetailsQuery, Response<PaymentYearsGetFlatPaymentsForPaymentYearsResponseDto>>
        {
            private readonly ILogger<PaymentYearsGetFlatPaymentsForPaymentYearsDetailsHandler> _logger;
            private readonly IPaymentYearsRepository _repository;
            public PaymentYearsGetFlatPaymentsForPaymentYearsDetailsHandler(ILogger<PaymentYearsGetFlatPaymentsForPaymentYearsDetailsHandler> logger, IPaymentYearsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentYearsGetFlatPaymentsForPaymentYearsResponseDto>> Handle(PaymentYearsGetFlatPaymentsForPaymentYearsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentYearsGetFlatPaymentsForPaymentYearsDetailsAsync(request.PaymentYearsId, request.FlatPaymentsId);
                return returnValue.ToResponse();
            }
        }
    }
}