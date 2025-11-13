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
using QuickCode.Demo.UserManagerModule.Application.Dtos.ApiPermissionGroup;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.ApiPermissionGroup
{
    public class UpdateApiPermissionGroupCommand : IRequest<Response<bool>>
    {
        public string PermissionGroupName { get; set; }
        public string ApiMethodDefinitionKey { get; set; }
        public ApiPermissionGroupDto request { get; set; }

        public UpdateApiPermissionGroupCommand(string permissionGroupName, string apiMethodDefinitionKey, ApiPermissionGroupDto request)
        {
            this.request = request;
            this.PermissionGroupName = permissionGroupName;
            this.ApiMethodDefinitionKey = apiMethodDefinitionKey;
        }

        public class UpdateApiPermissionGroupHandler : IRequestHandler<UpdateApiPermissionGroupCommand, Response<bool>>
        {
            private readonly ILogger<UpdateApiPermissionGroupHandler> _logger;
            private readonly IApiPermissionGroupRepository _repository;
            public UpdateApiPermissionGroupHandler(ILogger<UpdateApiPermissionGroupHandler> logger, IApiPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateApiPermissionGroupCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.PermissionGroupName, request.ApiMethodDefinitionKey);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}