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
    public class FlatPaymentsGetTotalPaidAmountByFlatQuery : IRequest<Response<FlatPaymentsGetTotalPaidAmountByFlatResponseDto>>
    {
        public int FlatPaymentsFlatId { get; set; }
        public bool FlatPaymentsPaid { get; set; }

        public FlatPaymentsGetTotalPaidAmountByFlatQuery(int flatPaymentsFlatId, bool flatPaymentsPaid)
        {
            this.FlatPaymentsFlatId = flatPaymentsFlatId;
            this.FlatPaymentsPaid = flatPaymentsPaid;
        }

        public class FlatPaymentsGetTotalPaidAmountByFlatHandler : IRequestHandler<FlatPaymentsGetTotalPaidAmountByFlatQuery, Response<FlatPaymentsGetTotalPaidAmountByFlatResponseDto>>
        {
            private readonly ILogger<FlatPaymentsGetTotalPaidAmountByFlatHandler> _logger;
            private readonly IFlatPaymentsRepository _repository;
            public FlatPaymentsGetTotalPaidAmountByFlatHandler(ILogger<FlatPaymentsGetTotalPaidAmountByFlatHandler> logger, IFlatPaymentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FlatPaymentsGetTotalPaidAmountByFlatResponseDto>> Handle(FlatPaymentsGetTotalPaidAmountByFlatQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatPaymentsGetTotalPaidAmountByFlatAsync(request.FlatPaymentsFlatId, request.FlatPaymentsPaid);
                return returnValue.ToResponse();
            }
        }
    }
}