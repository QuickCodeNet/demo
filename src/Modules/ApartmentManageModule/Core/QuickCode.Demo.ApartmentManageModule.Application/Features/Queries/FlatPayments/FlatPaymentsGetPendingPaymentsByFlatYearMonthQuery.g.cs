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
    public class FlatPaymentsGetPendingPaymentsByFlatYearMonthQuery : IRequest<Response<List<FlatPaymentsGetPendingPaymentsByFlatYearMonthResponseDto>>>
    {
        public int FlatPaymentsSiteId { get; set; }
        public bool FlatPaymentsPaid { get; set; }

        public FlatPaymentsGetPendingPaymentsByFlatYearMonthQuery(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            this.FlatPaymentsSiteId = flatPaymentsSiteId;
            this.FlatPaymentsPaid = flatPaymentsPaid;
        }

        public class FlatPaymentsGetPendingPaymentsByFlatYearMonthHandler : IRequestHandler<FlatPaymentsGetPendingPaymentsByFlatYearMonthQuery, Response<List<FlatPaymentsGetPendingPaymentsByFlatYearMonthResponseDto>>>
        {
            private readonly ILogger<FlatPaymentsGetPendingPaymentsByFlatYearMonthHandler> _logger;
            private readonly IFlatPaymentsRepository _repository;
            public FlatPaymentsGetPendingPaymentsByFlatYearMonthHandler(ILogger<FlatPaymentsGetPendingPaymentsByFlatYearMonthHandler> logger, IFlatPaymentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatPaymentsGetPendingPaymentsByFlatYearMonthResponseDto>>> Handle(FlatPaymentsGetPendingPaymentsByFlatYearMonthQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatPaymentsGetPendingPaymentsByFlatYearMonthAsync(request.FlatPaymentsSiteId, request.FlatPaymentsPaid);
                return returnValue.ToResponse();
            }
        }
    }
}