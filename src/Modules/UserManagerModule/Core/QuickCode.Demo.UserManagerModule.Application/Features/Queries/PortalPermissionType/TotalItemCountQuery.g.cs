using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionType;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionType
{
    public class TotalCountPortalPermissionTypeQuery : IRequest<Response<int>>
    {
        public TotalCountPortalPermissionTypeQuery()
        {
        }

        public class TotalCountPortalPermissionTypeHandler : IRequestHandler<TotalCountPortalPermissionTypeQuery, Response<int>>
        {
            private readonly ILogger<TotalCountPortalPermissionTypeHandler> _logger;
            private readonly IPortalPermissionTypeRepository _repository;
            public TotalCountPortalPermissionTypeHandler(ILogger<TotalCountPortalPermissionTypeHandler> logger, IPortalPermissionTypeRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountPortalPermissionTypeQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}