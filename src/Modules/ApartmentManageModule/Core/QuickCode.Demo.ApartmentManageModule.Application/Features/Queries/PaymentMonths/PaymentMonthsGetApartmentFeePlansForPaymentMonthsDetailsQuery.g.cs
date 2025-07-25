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
    public class PaymentMonthsGetApartmentFeePlansForPaymentMonthsDetailsQuery : IRequest<Response<PaymentMonthsGetApartmentFeePlansForPaymentMonthsResponseDto>>
    {
        public int PaymentMonthsId { get; set; }
        public int ApartmentFeePlansId { get; set; }

        public PaymentMonthsGetApartmentFeePlansForPaymentMonthsDetailsQuery(int paymentMonthsId, int apartmentFeePlansId)
        {
            this.PaymentMonthsId = paymentMonthsId;
            this.ApartmentFeePlansId = apartmentFeePlansId;
        }

        public class PaymentMonthsGetApartmentFeePlansForPaymentMonthsDetailsHandler : IRequestHandler<PaymentMonthsGetApartmentFeePlansForPaymentMonthsDetailsQuery, Response<PaymentMonthsGetApartmentFeePlansForPaymentMonthsResponseDto>>
        {
            private readonly ILogger<PaymentMonthsGetApartmentFeePlansForPaymentMonthsDetailsHandler> _logger;
            private readonly IPaymentMonthsRepository _repository;
            public PaymentMonthsGetApartmentFeePlansForPaymentMonthsDetailsHandler(ILogger<PaymentMonthsGetApartmentFeePlansForPaymentMonthsDetailsHandler> logger, IPaymentMonthsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentMonthsGetApartmentFeePlansForPaymentMonthsResponseDto>> Handle(PaymentMonthsGetApartmentFeePlansForPaymentMonthsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentMonthsGetApartmentFeePlansForPaymentMonthsDetailsAsync(request.PaymentMonthsId, request.ApartmentFeePlansId);
                return returnValue.ToResponse();
            }
        }
    }
}