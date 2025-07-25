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
    public class FlatExpenseInstallmentsGetFlatTotalDebtQuery : IRequest<Response<List<FlatExpenseInstallmentsGetFlatTotalDebtResponseDto>>>
    {
        public int FlatExpenseInstallmentsFlatId { get; set; }
        public bool FlatExpenseInstallmentsPaid { get; set; }

        public FlatExpenseInstallmentsGetFlatTotalDebtQuery(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid)
        {
            this.FlatExpenseInstallmentsFlatId = flatExpenseInstallmentsFlatId;
            this.FlatExpenseInstallmentsPaid = flatExpenseInstallmentsPaid;
        }

        public class FlatExpenseInstallmentsGetFlatTotalDebtHandler : IRequestHandler<FlatExpenseInstallmentsGetFlatTotalDebtQuery, Response<List<FlatExpenseInstallmentsGetFlatTotalDebtResponseDto>>>
        {
            private readonly ILogger<FlatExpenseInstallmentsGetFlatTotalDebtHandler> _logger;
            private readonly IFlatExpenseInstallmentsRepository _repository;
            public FlatExpenseInstallmentsGetFlatTotalDebtHandler(ILogger<FlatExpenseInstallmentsGetFlatTotalDebtHandler> logger, IFlatExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatExpenseInstallmentsGetFlatTotalDebtResponseDto>>> Handle(FlatExpenseInstallmentsGetFlatTotalDebtQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatExpenseInstallmentsGetFlatTotalDebtAsync(request.FlatExpenseInstallmentsFlatId, request.FlatExpenseInstallmentsPaid);
                return returnValue.ToResponse();
            }
        }
    }
}