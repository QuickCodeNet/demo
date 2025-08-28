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
    public class FlatPaymentsGetPaymentsByFlatYearMonthQuery : IRequest<Response<List<FlatPaymentsGetPaymentsByFlatYearMonthResponseDto>>>
    {
        public int FlatPaymentsSiteId { get; set; }
        public int FlatPaymentsFlatId { get; set; }
        public int FlatPaymentsYearId { get; set; }
        public int FlatPaymentsMonthId { get; set; }

        public FlatPaymentsGetPaymentsByFlatYearMonthQuery(int flatPaymentsSiteId, int flatPaymentsFlatId, int flatPaymentsYearId, int flatPaymentsMonthId)
        {
            this.FlatPaymentsSiteId = flatPaymentsSiteId;
            this.FlatPaymentsFlatId = flatPaymentsFlatId;
            this.FlatPaymentsYearId = flatPaymentsYearId;
            this.FlatPaymentsMonthId = flatPaymentsMonthId;
        }

        public class FlatPaymentsGetPaymentsByFlatYearMonthHandler : IRequestHandler<FlatPaymentsGetPaymentsByFlatYearMonthQuery, Response<List<FlatPaymentsGetPaymentsByFlatYearMonthResponseDto>>>
        {
            private readonly ILogger<FlatPaymentsGetPaymentsByFlatYearMonthHandler> _logger;
            private readonly IFlatPaymentsRepository _repository;
            public FlatPaymentsGetPaymentsByFlatYearMonthHandler(ILogger<FlatPaymentsGetPaymentsByFlatYearMonthHandler> logger, IFlatPaymentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatPaymentsGetPaymentsByFlatYearMonthResponseDto>>> Handle(FlatPaymentsGetPaymentsByFlatYearMonthQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatPaymentsGetPaymentsByFlatYearMonthAsync(request.FlatPaymentsSiteId, request.FlatPaymentsFlatId, request.FlatPaymentsYearId, request.FlatPaymentsMonthId);
                return returnValue.ToResponse();
            }
        }
    }
}