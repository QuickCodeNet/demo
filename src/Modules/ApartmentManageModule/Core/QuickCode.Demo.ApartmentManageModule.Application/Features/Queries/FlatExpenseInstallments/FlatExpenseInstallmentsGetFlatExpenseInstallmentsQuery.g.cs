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
    public class FlatExpenseInstallmentsGetFlatExpenseInstallmentsQuery : IRequest<Response<List<FlatExpenseInstallmentsGetFlatExpenseInstallmentsResponseDto>>>
    {
        public int FlatExpenseInstallmentsFlatId { get; set; }
        public int FlatExpenseInstallmentsExpenseId { get; set; }

        public FlatExpenseInstallmentsGetFlatExpenseInstallmentsQuery(int flatExpenseInstallmentsFlatId, int flatExpenseInstallmentsExpenseId)
        {
            this.FlatExpenseInstallmentsFlatId = flatExpenseInstallmentsFlatId;
            this.FlatExpenseInstallmentsExpenseId = flatExpenseInstallmentsExpenseId;
        }

        public class FlatExpenseInstallmentsGetFlatExpenseInstallmentsHandler : IRequestHandler<FlatExpenseInstallmentsGetFlatExpenseInstallmentsQuery, Response<List<FlatExpenseInstallmentsGetFlatExpenseInstallmentsResponseDto>>>
        {
            private readonly ILogger<FlatExpenseInstallmentsGetFlatExpenseInstallmentsHandler> _logger;
            private readonly IFlatExpenseInstallmentsRepository _repository;
            public FlatExpenseInstallmentsGetFlatExpenseInstallmentsHandler(ILogger<FlatExpenseInstallmentsGetFlatExpenseInstallmentsHandler> logger, IFlatExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatExpenseInstallmentsGetFlatExpenseInstallmentsResponseDto>>> Handle(FlatExpenseInstallmentsGetFlatExpenseInstallmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatExpenseInstallmentsGetFlatExpenseInstallmentsAsync(request.FlatExpenseInstallmentsFlatId, request.FlatExpenseInstallmentsExpenseId);
                return returnValue.ToResponse();
            }
        }
    }
}