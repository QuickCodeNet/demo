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
    public class PaymentMonthsGetFlatPaymentsForPaymentMonthsDetailsQuery : IRequest<Response<PaymentMonthsGetFlatPaymentsForPaymentMonthsResponseDto>>
    {
        public int PaymentMonthsId { get; set; }
        public int FlatPaymentsId { get; set; }

        public PaymentMonthsGetFlatPaymentsForPaymentMonthsDetailsQuery(int paymentMonthsId, int flatPaymentsId)
        {
            this.PaymentMonthsId = paymentMonthsId;
            this.FlatPaymentsId = flatPaymentsId;
        }

        public class PaymentMonthsGetFlatPaymentsForPaymentMonthsDetailsHandler : IRequestHandler<PaymentMonthsGetFlatPaymentsForPaymentMonthsDetailsQuery, Response<PaymentMonthsGetFlatPaymentsForPaymentMonthsResponseDto>>
        {
            private readonly ILogger<PaymentMonthsGetFlatPaymentsForPaymentMonthsDetailsHandler> _logger;
            private readonly IPaymentMonthsRepository _repository;
            public PaymentMonthsGetFlatPaymentsForPaymentMonthsDetailsHandler(ILogger<PaymentMonthsGetFlatPaymentsForPaymentMonthsDetailsHandler> logger, IPaymentMonthsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentMonthsGetFlatPaymentsForPaymentMonthsResponseDto>> Handle(PaymentMonthsGetFlatPaymentsForPaymentMonthsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PaymentMonthsGetFlatPaymentsForPaymentMonthsDetailsAsync(request.PaymentMonthsId, request.FlatPaymentsId);
                return returnValue.ToResponse();
            }
        }
    }
}