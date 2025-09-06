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
    public class FlatPaymentsGetUnpaidPaymentsByFlatQuery : IRequest<Response<List<FlatPaymentsGetUnpaidPaymentsByFlatResponseDto>>>
    {
        public int FlatPaymentsSiteId { get; set; }
        public int FlatPaymentsFlatId { get; set; }
        public bool FlatPaymentsPaid { get; set; }

        public FlatPaymentsGetUnpaidPaymentsByFlatQuery(int flatPaymentsSiteId, int flatPaymentsFlatId, bool flatPaymentsPaid)
        {
            this.FlatPaymentsSiteId = flatPaymentsSiteId;
            this.FlatPaymentsFlatId = flatPaymentsFlatId;
            this.FlatPaymentsPaid = flatPaymentsPaid;
        }

        public class FlatPaymentsGetUnpaidPaymentsByFlatHandler : IRequestHandler<FlatPaymentsGetUnpaidPaymentsByFlatQuery, Response<List<FlatPaymentsGetUnpaidPaymentsByFlatResponseDto>>>
        {
            private readonly ILogger<FlatPaymentsGetUnpaidPaymentsByFlatHandler> _logger;
            private readonly IFlatPaymentsRepository _repository;
            public FlatPaymentsGetUnpaidPaymentsByFlatHandler(ILogger<FlatPaymentsGetUnpaidPaymentsByFlatHandler> logger, IFlatPaymentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatPaymentsGetUnpaidPaymentsByFlatResponseDto>>> Handle(FlatPaymentsGetUnpaidPaymentsByFlatQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatPaymentsGetUnpaidPaymentsByFlatAsync(request.FlatPaymentsSiteId, request.FlatPaymentsFlatId, request.FlatPaymentsPaid);
                return returnValue.ToResponse();
            }
        }
    }
}