using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionGroup;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionGroup
{
    public class InsertPortalPermissionGroupCommand : IRequest<Response<PortalPermissionGroupDto>>
    {
        public PortalPermissionGroupDto request { get; set; }

        public InsertPortalPermissionGroupCommand(PortalPermissionGroupDto request)
        {
            this.request = request;
        }

        public class InsertPortalPermissionGroupHandler : IRequestHandler<InsertPortalPermissionGroupCommand, Response<PortalPermissionGroupDto>>
        {
            private readonly ILogger<InsertPortalPermissionGroupHandler> _logger;
            private readonly IPortalPermissionGroupRepository _repository;
            public InsertPortalPermissionGroupHandler(ILogger<InsertPortalPermissionGroupHandler> logger, IPortalPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionGroupDto>> Handle(InsertPortalPermissionGroupCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}