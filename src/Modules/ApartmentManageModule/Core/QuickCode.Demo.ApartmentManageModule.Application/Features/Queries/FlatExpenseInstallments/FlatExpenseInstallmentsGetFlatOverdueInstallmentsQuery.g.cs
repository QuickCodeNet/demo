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
    public class FlatExpenseInstallmentsGetFlatOverdueInstallmentsQuery : IRequest<Response<List<FlatExpenseInstallmentsGetFlatOverdueInstallmentsResponseDto>>>
    {
        public int FlatExpenseInstallmentsFlatId { get; set; }
        public bool FlatExpenseInstallmentsPaid { get; set; }

        public FlatExpenseInstallmentsGetFlatOverdueInstallmentsQuery(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid)
        {
            this.FlatExpenseInstallmentsFlatId = flatExpenseInstallmentsFlatId;
            this.FlatExpenseInstallmentsPaid = flatExpenseInstallmentsPaid;
        }

        public class FlatExpenseInstallmentsGetFlatOverdueInstallmentsHandler : IRequestHandler<FlatExpenseInstallmentsGetFlatOverdueInstallmentsQuery, Response<List<FlatExpenseInstallmentsGetFlatOverdueInstallmentsResponseDto>>>
        {
            private readonly ILogger<FlatExpenseInstallmentsGetFlatOverdueInstallmentsHandler> _logger;
            private readonly IFlatExpenseInstallmentsRepository _repository;
            public FlatExpenseInstallmentsGetFlatOverdueInstallmentsHandler(ILogger<FlatExpenseInstallmentsGetFlatOverdueInstallmentsHandler> logger, IFlatExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatExpenseInstallmentsGetFlatOverdueInstallmentsResponseDto>>> Handle(FlatExpenseInstallmentsGetFlatOverdueInstallmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatExpenseInstallmentsGetFlatOverdueInstallmentsAsync(request.FlatExpenseInstallmentsFlatId, request.FlatExpenseInstallmentsPaid);
                return returnValue.ToResponse();
            }
        }
    }
}