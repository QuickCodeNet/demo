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
    public class ExpenseTypesGetActiveExpenseTypesQuery : IRequest<Response<List<ExpenseTypesGetActiveExpenseTypesResponseDto>>>
    {
        public bool ExpenseTypesIsActive { get; set; }

        public ExpenseTypesGetActiveExpenseTypesQuery(bool expenseTypesIsActive)
        {
            this.ExpenseTypesIsActive = expenseTypesIsActive;
        }

        public class ExpenseTypesGetActiveExpenseTypesHandler : IRequestHandler<ExpenseTypesGetActiveExpenseTypesQuery, Response<List<ExpenseTypesGetActiveExpenseTypesResponseDto>>>
        {
            private readonly ILogger<ExpenseTypesGetActiveExpenseTypesHandler> _logger;
            private readonly IExpenseTypesRepository _repository;
            public ExpenseTypesGetActiveExpenseTypesHandler(ILogger<ExpenseTypesGetActiveExpenseTypesHandler> logger, IExpenseTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ExpenseTypesGetActiveExpenseTypesResponseDto>>> Handle(ExpenseTypesGetActiveExpenseTypesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExpenseTypesGetActiveExpenseTypesAsync(request.ExpenseTypesIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}