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
    public class CommonExpensesGetExpenseInstallmentsForCommonExpensesQuery : IRequest<Response<List<CommonExpensesGetExpenseInstallmentsForCommonExpensesResponseDto>>>
    {
        public int CommonExpensesId { get; set; }

        public CommonExpensesGetExpenseInstallmentsForCommonExpensesQuery(int commonExpensesId)
        {
            this.CommonExpensesId = commonExpensesId;
        }

        public class CommonExpensesGetExpenseInstallmentsForCommonExpensesHandler : IRequestHandler<CommonExpensesGetExpenseInstallmentsForCommonExpensesQuery, Response<List<CommonExpensesGetExpenseInstallmentsForCommonExpensesResponseDto>>>
        {
            private readonly ILogger<CommonExpensesGetExpenseInstallmentsForCommonExpensesHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesGetExpenseInstallmentsForCommonExpensesHandler(ILogger<CommonExpensesGetExpenseInstallmentsForCommonExpensesHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<CommonExpensesGetExpenseInstallmentsForCommonExpensesResponseDto>>> Handle(CommonExpensesGetExpenseInstallmentsForCommonExpensesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CommonExpensesGetExpenseInstallmentsForCommonExpensesAsync(request.CommonExpensesId);
                return returnValue.ToResponse();
            }
        }
    }
}