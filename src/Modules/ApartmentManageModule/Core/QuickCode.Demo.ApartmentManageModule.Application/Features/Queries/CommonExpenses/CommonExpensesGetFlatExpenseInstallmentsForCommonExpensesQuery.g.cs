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
    public class CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesQuery : IRequest<Response<List<CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesResponseDto>>>
    {
        public int CommonExpensesId { get; set; }

        public CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesQuery(int commonExpensesId)
        {
            this.CommonExpensesId = commonExpensesId;
        }

        public class CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesHandler : IRequestHandler<CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesQuery, Response<List<CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesResponseDto>>>
        {
            private readonly ILogger<CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesHandler(ILogger<CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesResponseDto>>> Handle(CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesAsync(request.CommonExpensesId);
                return returnValue.ToResponse();
            }
        }
    }
}