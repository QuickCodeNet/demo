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
    public class FlatPaymentsGetFlatPaymentsByMonthQuery : IRequest<Response<List<FlatPaymentsGetFlatPaymentsByMonthResponseDto>>>
    {
        public int FlatPaymentsFlatId { get; set; }
        public int FlatPaymentsYearId { get; set; }
        public int FlatPaymentsMonthId { get; set; }

        public FlatPaymentsGetFlatPaymentsByMonthQuery(int flatPaymentsFlatId, int flatPaymentsYearId, int flatPaymentsMonthId)
        {
            this.FlatPaymentsFlatId = flatPaymentsFlatId;
            this.FlatPaymentsYearId = flatPaymentsYearId;
            this.FlatPaymentsMonthId = flatPaymentsMonthId;
        }

        public class FlatPaymentsGetFlatPaymentsByMonthHandler : IRequestHandler<FlatPaymentsGetFlatPaymentsByMonthQuery, Response<List<FlatPaymentsGetFlatPaymentsByMonthResponseDto>>>
        {
            private readonly ILogger<FlatPaymentsGetFlatPaymentsByMonthHandler> _logger;
            private readonly IFlatPaymentsRepository _repository;
            public FlatPaymentsGetFlatPaymentsByMonthHandler(ILogger<FlatPaymentsGetFlatPaymentsByMonthHandler> logger, IFlatPaymentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatPaymentsGetFlatPaymentsByMonthResponseDto>>> Handle(FlatPaymentsGetFlatPaymentsByMonthQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatPaymentsGetFlatPaymentsByMonthAsync(request.FlatPaymentsFlatId, request.FlatPaymentsYearId, request.FlatPaymentsMonthId);
                return returnValue.ToResponse();
            }
        }
    }
}