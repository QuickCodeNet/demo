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
    public class PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsQuery : IRequest<Response<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsResponseDto>>
    {
        public int PermissionGroupsId { get; set; }
        public int ApiPermissionGroupsId { get; set; }

        public PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsQuery(int permissionGroupsId, int apiPermissionGroupsId)
        {
            this.PermissionGroupsId = permissionGroupsId;
            this.ApiPermissionGroupsId = apiPermissionGroupsId;
        }

        public class PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsHandler : IRequestHandler<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsQuery, Response<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsResponseDto>>
        {
            private readonly ILogger<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsHandler(ILogger<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsResponseDto>> Handle(PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsAsync(request.PermissionGroupsId, request.ApiPermissionGroupsId);
                return returnValue.ToResponse();
            }
        }
    }
}