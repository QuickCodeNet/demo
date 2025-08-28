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
    public class CommonExpensesGetExpensesBySiteQuery : IRequest<Response<List<CommonExpensesGetExpensesBySiteResponseDto>>>
    {
        public int CommonExpensesSiteId { get; set; }

        public CommonExpensesGetExpensesBySiteQuery(int commonExpensesSiteId)
        {
            this.CommonExpensesSiteId = commonExpensesSiteId;
        }

        public class CommonExpensesGetExpensesBySiteHandler : IRequestHandler<CommonExpensesGetExpensesBySiteQuery, Response<List<CommonExpensesGetExpensesBySiteResponseDto>>>
        {
            private readonly ILogger<CommonExpensesGetExpensesBySiteHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesGetExpensesBySiteHandler(ILogger<CommonExpensesGetExpensesBySiteHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<CommonExpensesGetExpensesBySiteResponseDto>>> Handle(CommonExpensesGetExpensesBySiteQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CommonExpensesGetExpensesBySiteAsync(request.CommonExpensesSiteId);
                return returnValue.ToResponse();
            }
        }
    }
}