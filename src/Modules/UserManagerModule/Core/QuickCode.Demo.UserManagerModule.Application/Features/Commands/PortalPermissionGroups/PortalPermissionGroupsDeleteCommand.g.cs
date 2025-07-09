using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class PortalPermissionGroupsDeleteCommand : IRequest<Response<bool>>
    {
        public PortalPermissionGroupsDto request { get; set; }

        public PortalPermissionGroupsDeleteCommand(PortalPermissionGroupsDto request)
        {
            this.request = request;
        }

        public class PortalPermissionGroupsDeleteHandler : IRequestHandler<PortalPermissionGroupsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<PortalPermissionGroupsDeleteHandler> _logger;
            private readonly IPortalPermissionGroupsRepository _repository;
            public PortalPermissionGroupsDeleteHandler(ILogger<PortalPermissionGroupsDeleteHandler> logger, IPortalPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PortalPermissionGroupsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}