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
    public class FlatPaymentsGetUnpaidPaymentsBySiteQuery : IRequest<Response<List<FlatPaymentsGetUnpaidPaymentsBySiteResponseDto>>>
    {
        public int FlatPaymentsSiteId { get; set; }
        public bool FlatPaymentsPaid { get; set; }

        public FlatPaymentsGetUnpaidPaymentsBySiteQuery(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            this.FlatPaymentsSiteId = flatPaymentsSiteId;
            this.FlatPaymentsPaid = flatPaymentsPaid;
        }

        public class FlatPaymentsGetUnpaidPaymentsBySiteHandler : IRequestHandler<FlatPaymentsGetUnpaidPaymentsBySiteQuery, Response<List<FlatPaymentsGetUnpaidPaymentsBySiteResponseDto>>>
        {
            private readonly ILogger<FlatPaymentsGetUnpaidPaymentsBySiteHandler> _logger;
            private readonly IFlatPaymentsRepository _repository;
            public FlatPaymentsGetUnpaidPaymentsBySiteHandler(ILogger<FlatPaymentsGetUnpaidPaymentsBySiteHandler> logger, IFlatPaymentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatPaymentsGetUnpaidPaymentsBySiteResponseDto>>> Handle(FlatPaymentsGetUnpaidPaymentsBySiteQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatPaymentsGetUnpaidPaymentsBySiteAsync(request.FlatPaymentsSiteId, request.FlatPaymentsPaid);
                return returnValue.ToResponse();
            }
        }
    }
}