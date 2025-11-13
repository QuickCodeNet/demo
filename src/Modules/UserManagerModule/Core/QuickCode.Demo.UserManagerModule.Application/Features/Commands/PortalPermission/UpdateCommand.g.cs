using System;
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
    public class UpdatePortalPermissionCommand : IRequest<Response<bool>>
    {
        public string Name { get; set; }
        public PortalPermissionDto request { get; set; }

        public UpdatePortalPermissionCommand(string name, PortalPermissionDto request)
        {
            this.request = request;
            this.Name = name;
        }

        public class UpdatePortalPermissionHandler : IRequestHandler<UpdatePortalPermissionCommand, Response<bool>>
        {
            private readonly ILogger<UpdatePortalPermissionHandler> _logger;
            private readonly IPortalPermissionRepository _repository;
            public UpdatePortalPermissionHandler(ILogger<UpdatePortalPermissionHandler> logger, IPortalPermissionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdatePortalPermissionCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Name);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}