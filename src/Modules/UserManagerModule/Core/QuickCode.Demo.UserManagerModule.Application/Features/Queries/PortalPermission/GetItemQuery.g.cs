using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermission;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalPermission
{
    public class GetItemPortalPermissionQuery : IRequest<Response<PortalPermissionDto>>
    {
        public string Name { get; set; }

        public GetItemPortalPermissionQuery(string name)
        {
            this.Name = name;
        }

        public class GetItemPortalPermissionHandler : IRequestHandler<GetItemPortalPermissionQuery, Response<PortalPermissionDto>>
        {
            private readonly ILogger<GetItemPortalPermissionHandler> _logger;
            private readonly IPortalPermissionRepository _repository;
            public GetItemPortalPermissionHandler(ILogger<GetItemPortalPermissionHandler> logger, IPortalPermissionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionDto>> Handle(GetItemPortalPermissionQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Name);
                return returnValue.ToResponse();
            }
        }
    }
}