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
    public class CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesDetailsQuery : IRequest<Response<CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesResponseDto>>
    {
        public int CommonExpensesId { get; set; }
        public int FlatExpenseInstallmentsId { get; set; }

        public CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesDetailsQuery(int commonExpensesId, int flatExpenseInstallmentsId)
        {
            this.CommonExpensesId = commonExpensesId;
            this.FlatExpenseInstallmentsId = flatExpenseInstallmentsId;
        }

        public class CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesDetailsHandler : IRequestHandler<CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesDetailsQuery, Response<CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesResponseDto>>
        {
            private readonly ILogger<CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesDetailsHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesDetailsHandler(ILogger<CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesDetailsHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesResponseDto>> Handle(CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CommonExpensesGetFlatExpenseInstallmentsForCommonExpensesDetailsAsync(request.CommonExpensesId, request.FlatExpenseInstallmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}