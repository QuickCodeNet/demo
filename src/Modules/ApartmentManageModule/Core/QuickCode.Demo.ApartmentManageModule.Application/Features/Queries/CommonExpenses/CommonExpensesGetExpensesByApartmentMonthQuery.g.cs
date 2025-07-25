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
    public class CommonExpensesGetExpensesByApartmentMonthQuery : IRequest<Response<List<CommonExpensesGetExpensesByApartmentMonthResponseDto>>>
    {
        public int CommonExpensesSiteId { get; set; }
        public int CommonExpensesApartmentId { get; set; }
        public int CommonExpensesYearId { get; set; }
        public int CommonExpensesMonthId { get; set; }

        public CommonExpensesGetExpensesByApartmentMonthQuery(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesYearId, int commonExpensesMonthId)
        {
            this.CommonExpensesSiteId = commonExpensesSiteId;
            this.CommonExpensesApartmentId = commonExpensesApartmentId;
            this.CommonExpensesYearId = commonExpensesYearId;
            this.CommonExpensesMonthId = commonExpensesMonthId;
        }

        public class CommonExpensesGetExpensesByApartmentMonthHandler : IRequestHandler<CommonExpensesGetExpensesByApartmentMonthQuery, Response<List<CommonExpensesGetExpensesByApartmentMonthResponseDto>>>
        {
            private readonly ILogger<CommonExpensesGetExpensesByApartmentMonthHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesGetExpensesByApartmentMonthHandler(ILogger<CommonExpensesGetExpensesByApartmentMonthHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<CommonExpensesGetExpensesByApartmentMonthResponseDto>>> Handle(CommonExpensesGetExpensesByApartmentMonthQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CommonExpensesGetExpensesByApartmentMonthAsync(request.CommonExpensesSiteId, request.CommonExpensesApartmentId, request.CommonExpensesYearId, request.CommonExpensesMonthId);
                return returnValue.ToResponse();
            }
        }
    }
}