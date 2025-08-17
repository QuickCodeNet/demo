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
    public class CommonExpensesGetExpensesByTypeQuery : IRequest<Response<List<CommonExpensesGetExpensesByTypeResponseDto>>>
    {
        public int CommonExpensesSiteId { get; set; }
        public int CommonExpensesApartmentId { get; set; }
        public int CommonExpensesExpenseTypeId { get; set; }
        public int CommonExpensesYearId { get; set; }
        public int CommonExpensesMonthId { get; set; }

        public CommonExpensesGetExpensesByTypeQuery(int commonExpensesSiteId, int commonExpensesApartmentId, int commonExpensesExpenseTypeId, int commonExpensesYearId, int commonExpensesMonthId)
        {
            this.CommonExpensesSiteId = commonExpensesSiteId;
            this.CommonExpensesApartmentId = commonExpensesApartmentId;
            this.CommonExpensesExpenseTypeId = commonExpensesExpenseTypeId;
            this.CommonExpensesYearId = commonExpensesYearId;
            this.CommonExpensesMonthId = commonExpensesMonthId;
        }

        public class CommonExpensesGetExpensesByTypeHandler : IRequestHandler<CommonExpensesGetExpensesByTypeQuery, Response<List<CommonExpensesGetExpensesByTypeResponseDto>>>
        {
            private readonly ILogger<CommonExpensesGetExpensesByTypeHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesGetExpensesByTypeHandler(ILogger<CommonExpensesGetExpensesByTypeHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<CommonExpensesGetExpensesByTypeResponseDto>>> Handle(CommonExpensesGetExpensesByTypeQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CommonExpensesGetExpensesByTypeAsync(request.CommonExpensesSiteId, request.CommonExpensesApartmentId, request.CommonExpensesExpenseTypeId, request.CommonExpensesYearId, request.CommonExpensesMonthId);
                return returnValue.ToResponse();
            }
        }
    }
}