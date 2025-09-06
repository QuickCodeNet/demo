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
    public class CommonExpensesListQuery : IRequest<Response<List<CommonExpensesDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public CommonExpensesListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class CommonExpensesListHandler : IRequestHandler<CommonExpensesListQuery, Response<List<CommonExpensesDto>>>
        {
            private readonly ILogger<CommonExpensesListHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesListHandler(ILogger<CommonExpensesListHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<CommonExpensesDto>>> Handle(CommonExpensesListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}