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
    public class FlatExpenseInstallmentsListQuery : IRequest<Response<List<FlatExpenseInstallmentsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public FlatExpenseInstallmentsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class FlatExpenseInstallmentsListHandler : IRequestHandler<FlatExpenseInstallmentsListQuery, Response<List<FlatExpenseInstallmentsDto>>>
        {
            private readonly ILogger<FlatExpenseInstallmentsListHandler> _logger;
            private readonly IFlatExpenseInstallmentsRepository _repository;
            public FlatExpenseInstallmentsListHandler(ILogger<FlatExpenseInstallmentsListHandler> logger, IFlatExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatExpenseInstallmentsDto>>> Handle(FlatExpenseInstallmentsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}