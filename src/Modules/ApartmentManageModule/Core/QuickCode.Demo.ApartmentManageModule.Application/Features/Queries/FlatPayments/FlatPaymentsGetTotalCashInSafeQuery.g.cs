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
    public class FlatPaymentsGetTotalCashInSafeQuery : IRequest<Response<List<FlatPaymentsGetTotalCashInSafeResponseDto>>>
    {
        public int FlatPaymentsSiteId { get; set; }
        public bool FlatPaymentsPaid { get; set; }

        public FlatPaymentsGetTotalCashInSafeQuery(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            this.FlatPaymentsSiteId = flatPaymentsSiteId;
            this.FlatPaymentsPaid = flatPaymentsPaid;
        }

        public class FlatPaymentsGetTotalCashInSafeHandler : IRequestHandler<FlatPaymentsGetTotalCashInSafeQuery, Response<List<FlatPaymentsGetTotalCashInSafeResponseDto>>>
        {
            private readonly ILogger<FlatPaymentsGetTotalCashInSafeHandler> _logger;
            private readonly IFlatPaymentsRepository _repository;
            public FlatPaymentsGetTotalCashInSafeHandler(ILogger<FlatPaymentsGetTotalCashInSafeHandler> logger, IFlatPaymentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatPaymentsGetTotalCashInSafeResponseDto>>> Handle(FlatPaymentsGetTotalCashInSafeQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatPaymentsGetTotalCashInSafeAsync(request.FlatPaymentsSiteId, request.FlatPaymentsPaid);
                return returnValue.ToResponse();
            }
        }
    }
}