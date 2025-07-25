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
    public class PaymentYearsGetApartmentFeePlansForPaymentYearsQuery : IRequest<Response<List<PaymentYearsGetApartmentFeePlansForPaymentYearsResponseDto>>>
    {
        public int PaymentYearsId { get; set; }

        public PaymentYearsGetApartmentFeePlansForPaymentYearsQuery(int paymentYearsId)
        {
            this.PaymentYearsId = paymentYearsId;
        }

        public class PaymentYearsGetApartmentFeePlansForPaymentYearsHandler : IRequestHandler<PaymentYearsGetApartmentFeePlansForPaymentYearsQuery, Response<List<PaymentYearsGetApartmentFeePlansForPaymentYearsResponseDto>>>
        {
            private readonly ILogger<PaymentYearsGetApartmentFeePlansForPaymentYearsHandler> _logger;
            private readonly IPaymentYearsRepository _repository;
            public PaymentYearsGetApartmentFeePlansForPaymentYearsHandler(ILogger<PaymentYearsGetApartmentFeePlansForPaymentYearsHandler> logger, IPaymentYearsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentYearsGetApartmentFeePlansForPaymentYearsResponseDto>>> Handle(PaymentYearsGetApartmentFeePlansForPaymentYearsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentYearsGetApartmentFeePlansForPaymentYearsAsync(request.PaymentYearsId);
                return returnValue.ToResponse();
            }
        }
    }
}