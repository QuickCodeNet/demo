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
    public class FlatPaymentsGetUnpaidPaymentsCountBySiteQuery : IRequest<Response<long>>
    {
        public int FlatPaymentsSiteId { get; set; }
        public bool FlatPaymentsPaid { get; set; }

        public FlatPaymentsGetUnpaidPaymentsCountBySiteQuery(int flatPaymentsSiteId, bool flatPaymentsPaid)
        {
            this.FlatPaymentsSiteId = flatPaymentsSiteId;
            this.FlatPaymentsPaid = flatPaymentsPaid;
        }

        public class FlatPaymentsGetUnpaidPaymentsCountBySiteHandler : IRequestHandler<FlatPaymentsGetUnpaidPaymentsCountBySiteQuery, Response<long>>
        {
            private readonly ILogger<FlatPaymentsGetUnpaidPaymentsCountBySiteHandler> _logger;
            private readonly IFlatPaymentsRepository _repository;
            public FlatPaymentsGetUnpaidPaymentsCountBySiteHandler(ILogger<FlatPaymentsGetUnpaidPaymentsCountBySiteHandler> logger, IFlatPaymentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(FlatPaymentsGetUnpaidPaymentsCountBySiteQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatPaymentsGetUnpaidPaymentsCountBySiteAsync(request.FlatPaymentsSiteId, request.FlatPaymentsPaid);
                return returnValue.ToResponse();
            }
        }
    }
}