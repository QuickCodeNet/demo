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
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class PermissionGroupsGetApiPermissionGroupsForPermissionGroupsQuery : IRequest<Response<List<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsResponseDto>>>
    {
        public int PermissionGroupsId { get; set; }

        public PermissionGroupsGetApiPermissionGroupsForPermissionGroupsQuery(int permissionGroupsId)
        {
            this.PermissionGroupsId = permissionGroupsId;
        }

        public class PermissionGroupsGetApiPermissionGroupsForPermissionGroupsHandler : IRequestHandler<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsQuery, Response<List<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsResponseDto>>>
        {
            private readonly ILogger<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsGetApiPermissionGroupsForPermissionGroupsHandler(ILogger<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsResponseDto>>> Handle(PermissionGroupsGetApiPermissionGroupsForPermissionGroupsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PermissionGroupsGetApiPermissionGroupsForPermissionGroupsAsync(request.PermissionGroupsId);
                return returnValue.ToResponse();
            }
        }
    }
}