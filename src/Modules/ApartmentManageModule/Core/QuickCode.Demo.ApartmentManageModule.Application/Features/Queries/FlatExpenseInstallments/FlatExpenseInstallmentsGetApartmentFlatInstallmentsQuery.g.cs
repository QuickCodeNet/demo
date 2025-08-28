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
    public class FlatExpenseInstallmentsGetApartmentFlatInstallmentsQuery : IRequest<Response<List<FlatExpenseInstallmentsGetApartmentFlatInstallmentsResponseDto>>>
    {
        public int FlatExpenseInstallmentsSiteId { get; set; }
        public int FlatExpenseInstallmentsApartmentId { get; set; }

        public FlatExpenseInstallmentsGetApartmentFlatInstallmentsQuery(int flatExpenseInstallmentsSiteId, int flatExpenseInstallmentsApartmentId)
        {
            this.FlatExpenseInstallmentsSiteId = flatExpenseInstallmentsSiteId;
            this.FlatExpenseInstallmentsApartmentId = flatExpenseInstallmentsApartmentId;
        }

        public class FlatExpenseInstallmentsGetApartmentFlatInstallmentsHandler : IRequestHandler<FlatExpenseInstallmentsGetApartmentFlatInstallmentsQuery, Response<List<FlatExpenseInstallmentsGetApartmentFlatInstallmentsResponseDto>>>
        {
            private readonly ILogger<FlatExpenseInstallmentsGetApartmentFlatInstallmentsHandler> _logger;
            private readonly IFlatExpenseInstallmentsRepository _repository;
            public FlatExpenseInstallmentsGetApartmentFlatInstallmentsHandler(ILogger<FlatExpenseInstallmentsGetApartmentFlatInstallmentsHandler> logger, IFlatExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatExpenseInstallmentsGetApartmentFlatInstallmentsResponseDto>>> Handle(FlatExpenseInstallmentsGetApartmentFlatInstallmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatExpenseInstallmentsGetApartmentFlatInstallmentsAsync(request.FlatExpenseInstallmentsSiteId, request.FlatExpenseInstallmentsApartmentId);
                return returnValue.ToResponse();
            }
        }
    }
}