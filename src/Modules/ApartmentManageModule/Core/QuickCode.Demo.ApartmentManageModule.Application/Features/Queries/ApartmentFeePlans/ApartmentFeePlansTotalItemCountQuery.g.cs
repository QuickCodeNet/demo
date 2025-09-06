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
    public class ApartmentFeePlansTotalItemCountQuery : IRequest<Response<int>>
    {
        public ApartmentFeePlansTotalItemCountQuery()
        {
        }

        public class ApartmentFeePlansTotalItemCountHandler : IRequestHandler<ApartmentFeePlansTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<ApartmentFeePlansTotalItemCountHandler> _logger;
            private readonly IApartmentFeePlansRepository _repository;
            public ApartmentFeePlansTotalItemCountHandler(ILogger<ApartmentFeePlansTotalItemCountHandler> logger, IApartmentFeePlansRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(ApartmentFeePlansTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}