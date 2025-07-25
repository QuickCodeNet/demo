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
    public class FlatExpenseInstallmentsGetItemQuery : IRequest<Response<FlatExpenseInstallmentsDto>>
    {
        public int Id { get; set; }

        public FlatExpenseInstallmentsGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class FlatExpenseInstallmentsGetItemHandler : IRequestHandler<FlatExpenseInstallmentsGetItemQuery, Response<FlatExpenseInstallmentsDto>>
        {
            private readonly ILogger<FlatExpenseInstallmentsGetItemHandler> _logger;
            private readonly IFlatExpenseInstallmentsRepository _repository;
            public FlatExpenseInstallmentsGetItemHandler(ILogger<FlatExpenseInstallmentsGetItemHandler> logger, IFlatExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FlatExpenseInstallmentsDto>> Handle(FlatExpenseInstallmentsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}