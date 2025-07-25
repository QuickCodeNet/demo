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
    public class PaymentYearsGetApartmentFeePlansForPaymentYearsDetailsQuery : IRequest<Response<PaymentYearsGetApartmentFeePlansForPaymentYearsResponseDto>>
    {
        public int PaymentYearsId { get; set; }
        public int ApartmentFeePlansId { get; set; }

        public PaymentYearsGetApartmentFeePlansForPaymentYearsDetailsQuery(int paymentYearsId, int apartmentFeePlansId)
        {
            this.PaymentYearsId = paymentYearsId;
            this.ApartmentFeePlansId = apartmentFeePlansId;
        }

        public class PaymentYearsGetApartmentFeePlansForPaymentYearsDetailsHandler : IRequestHandler<PaymentYearsGetApartmentFeePlansForPaymentYearsDetailsQuery, Response<PaymentYearsGetApartmentFeePlansForPaymentYearsResponseDto>>
        {
            private readonly ILogger<PaymentYearsGetApartmentFeePlansForPaymentYearsDetailsHandler> _logger;
            private readonly IPaymentYearsRepository _repository;
            public PaymentYearsGetApartmentFeePlansForPaymentYearsDetailsHandler(ILogger<PaymentYearsGetApartmentFeePlansForPaymentYearsDetailsHandler> logger, IPaymentYearsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentYearsGetApartmentFeePlansForPaymentYearsResponseDto>> Handle(PaymentYearsGetApartmentFeePlansForPaymentYearsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentYearsGetApartmentFeePlansForPaymentYearsDetailsAsync(request.PaymentYearsId, request.ApartmentFeePlansId);
                return returnValue.ToResponse();
            }
        }
    }
}