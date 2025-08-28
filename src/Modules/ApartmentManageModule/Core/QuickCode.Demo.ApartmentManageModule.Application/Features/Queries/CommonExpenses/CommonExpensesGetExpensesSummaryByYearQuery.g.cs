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
    public class CommonExpensesGetExpensesSummaryByYearQuery : IRequest<Response<List<CommonExpensesGetExpensesSummaryByYearResponseDto>>>
    {
        public int CommonExpensesSiteId { get; set; }
        public int CommonExpensesApartmentId { get; set; }
        public int CommonExpensesYearId { get; set; }

        public CommonExpensesGetExpensesSummaryByYearQuery(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesYearId)
        {
            this.CommonExpensesSiteId = commonExpensesSiteId;
            this.CommonExpensesApartmentId = commonExpensesApartmentId;
            this.CommonExpensesYearId = commonExpensesYearId;
        }

        public class CommonExpensesGetExpensesSummaryByYearHandler : IRequestHandler<CommonExpensesGetExpensesSummaryByYearQuery, Response<List<CommonExpensesGetExpensesSummaryByYearResponseDto>>>
        {
            private readonly ILogger<CommonExpensesGetExpensesSummaryByYearHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesGetExpensesSummaryByYearHandler(ILogger<CommonExpensesGetExpensesSummaryByYearHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<CommonExpensesGetExpensesSummaryByYearResponseDto>>> Handle(CommonExpensesGetExpensesSummaryByYearQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CommonExpensesGetExpensesSummaryByYearAsync(request.CommonExpensesSiteId, request.CommonExpensesApartmentId, request.CommonExpensesYearId);
                return returnValue.ToResponse();
            }
        }
    }
}