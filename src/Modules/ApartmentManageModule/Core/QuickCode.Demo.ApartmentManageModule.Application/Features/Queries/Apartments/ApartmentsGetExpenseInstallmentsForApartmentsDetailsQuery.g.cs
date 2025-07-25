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
    public class ApartmentsGetExpenseInstallmentsForApartmentsDetailsQuery : IRequest<Response<ApartmentsGetExpenseInstallmentsForApartmentsResponseDto>>
    {
        public int ApartmentsId { get; set; }
        public int ExpenseInstallmentsId { get; set; }

        public ApartmentsGetExpenseInstallmentsForApartmentsDetailsQuery(int apartmentsId, int expenseInstallmentsId)
        {
            this.ApartmentsId = apartmentsId;
            this.ExpenseInstallmentsId = expenseInstallmentsId;
        }

        public class ApartmentsGetExpenseInstallmentsForApartmentsDetailsHandler : IRequestHandler<ApartmentsGetExpenseInstallmentsForApartmentsDetailsQuery, Response<ApartmentsGetExpenseInstallmentsForApartmentsResponseDto>>
        {
            private readonly ILogger<ApartmentsGetExpenseInstallmentsForApartmentsDetailsHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetExpenseInstallmentsForApartmentsDetailsHandler(ILogger<ApartmentsGetExpenseInstallmentsForApartmentsDetailsHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApartmentsGetExpenseInstallmentsForApartmentsResponseDto>> Handle(ApartmentsGetExpenseInstallmentsForApartmentsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentsGetExpenseInstallmentsForApartmentsDetailsAsync(request.ApartmentsId, request.ExpenseInstallmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}