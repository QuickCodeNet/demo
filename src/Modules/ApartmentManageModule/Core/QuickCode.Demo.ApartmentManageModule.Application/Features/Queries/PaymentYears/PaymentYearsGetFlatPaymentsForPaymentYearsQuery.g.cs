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
    public class PaymentYearsGetFlatPaymentsForPaymentYearsQuery : IRequest<Response<List<PaymentYearsGetFlatPaymentsForPaymentYearsResponseDto>>>
    {
        public int PaymentYearsId { get; set; }

        public PaymentYearsGetFlatPaymentsForPaymentYearsQuery(int paymentYearsId)
        {
            this.PaymentYearsId = paymentYearsId;
        }

        public class PaymentYearsGetFlatPaymentsForPaymentYearsHandler : IRequestHandler<PaymentYearsGetFlatPaymentsForPaymentYearsQuery, Response<List<PaymentYearsGetFlatPaymentsForPaymentYearsResponseDto>>>
        {
            private readonly ILogger<PaymentYearsGetFlatPaymentsForPaymentYearsHandler> _logger;
            private readonly IPaymentYearsRepository _repository;
            public PaymentYearsGetFlatPaymentsForPaymentYearsHandler(ILogger<PaymentYearsGetFlatPaymentsForPaymentYearsHandler> logger, IPaymentYearsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentYearsGetFlatPaymentsForPaymentYearsResponseDto>>> Handle(PaymentYearsGetFlatPaymentsForPaymentYearsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentYearsGetFlatPaymentsForPaymentYearsAsync(request.PaymentYearsId);
                return returnValue.ToResponse();
            }
        }
    }
}