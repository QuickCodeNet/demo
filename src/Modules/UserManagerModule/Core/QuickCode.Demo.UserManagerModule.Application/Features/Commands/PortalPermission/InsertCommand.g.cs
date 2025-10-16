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
    public class InsertPortalPermissionCommand : IRequest<Response<PortalPermissionDto>>
    {
        public PortalPermissionDto request { get; set; }

        public InsertPortalPermissionCommand(PortalPermissionDto request)
        {
            this.request = request;
        }

        public class InsertPortalPermissionHandler : IRequestHandler<InsertPortalPermissionCommand, Response<PortalPermissionDto>>
        {
            private readonly ILogger<InsertPortalPermissionHandler> _logger;
            private readonly IPortalPermissionRepository _repository;
            public InsertPortalPermissionHandler(ILogger<InsertPortalPermissionHandler> logger, IPortalPermissionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionDto>> Handle(InsertPortalPermissionCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}