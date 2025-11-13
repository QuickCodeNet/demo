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
    public class DeleteApiPermissionGroupCommand : IRequest<Response<bool>>
    {
        public ApiPermissionGroupDto request { get; set; }

        public DeleteApiPermissionGroupCommand(ApiPermissionGroupDto request)
        {
            this.request = request;
        }

        public class DeleteApiPermissionGroupHandler : IRequestHandler<DeleteApiPermissionGroupCommand, Response<bool>>
        {
            private readonly ILogger<DeleteApiPermissionGroupHandler> _logger;
            private readonly IApiPermissionGroupRepository _repository;
            public DeleteApiPermissionGroupHandler(ILogger<DeleteApiPermissionGroupHandler> logger, IApiPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteApiPermissionGroupCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}