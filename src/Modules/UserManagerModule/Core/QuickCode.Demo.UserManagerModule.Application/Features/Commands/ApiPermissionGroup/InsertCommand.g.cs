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
    public class InsertApiPermissionGroupCommand : IRequest<Response<ApiPermissionGroupDto>>
    {
        public ApiPermissionGroupDto request { get; set; }

        public InsertApiPermissionGroupCommand(ApiPermissionGroupDto request)
        {
            this.request = request;
        }

        public class InsertApiPermissionGroupHandler : IRequestHandler<InsertApiPermissionGroupCommand, Response<ApiPermissionGroupDto>>
        {
            private readonly ILogger<InsertApiPermissionGroupHandler> _logger;
            private readonly IApiPermissionGroupRepository _repository;
            public InsertApiPermissionGroupHandler(ILogger<InsertApiPermissionGroupHandler> logger, IApiPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApiPermissionGroupDto>> Handle(InsertApiPermissionGroupCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}