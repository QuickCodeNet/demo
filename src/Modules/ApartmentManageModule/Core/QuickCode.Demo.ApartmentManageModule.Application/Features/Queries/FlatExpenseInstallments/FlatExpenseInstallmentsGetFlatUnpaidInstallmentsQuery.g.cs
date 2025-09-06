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
    public class FlatExpenseInstallmentsGetFlatUnpaidInstallmentsQuery : IRequest<Response<List<FlatExpenseInstallmentsGetFlatUnpaidInstallmentsResponseDto>>>
    {
        public int FlatExpenseInstallmentsFlatId { get; set; }
        public bool FlatExpenseInstallmentsPaid { get; set; }

        public FlatExpenseInstallmentsGetFlatUnpaidInstallmentsQuery(int flatExpenseInstallmentsFlatId, bool flatExpenseInstallmentsPaid)
        {
            this.FlatExpenseInstallmentsFlatId = flatExpenseInstallmentsFlatId;
            this.FlatExpenseInstallmentsPaid = flatExpenseInstallmentsPaid;
        }

        public class FlatExpenseInstallmentsGetFlatUnpaidInstallmentsHandler : IRequestHandler<FlatExpenseInstallmentsGetFlatUnpaidInstallmentsQuery, Response<List<FlatExpenseInstallmentsGetFlatUnpaidInstallmentsResponseDto>>>
        {
            private readonly ILogger<FlatExpenseInstallmentsGetFlatUnpaidInstallmentsHandler> _logger;
            private readonly IFlatExpenseInstallmentsRepository _repository;
            public FlatExpenseInstallmentsGetFlatUnpaidInstallmentsHandler(ILogger<FlatExpenseInstallmentsGetFlatUnpaidInstallmentsHandler> logger, IFlatExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatExpenseInstallmentsGetFlatUnpaidInstallmentsResponseDto>>> Handle(FlatExpenseInstallmentsGetFlatUnpaidInstallmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatExpenseInstallmentsGetFlatUnpaidInstallmentsAsync(request.FlatExpenseInstallmentsFlatId, request.FlatExpenseInstallmentsPaid);
                return returnValue.ToResponse();
            }
        }
    }
}