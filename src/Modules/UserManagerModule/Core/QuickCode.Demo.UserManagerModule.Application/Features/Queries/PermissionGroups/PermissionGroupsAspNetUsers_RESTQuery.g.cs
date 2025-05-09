using System;
using System.Linq;
using MediatR;
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
    public class PermissionGroupsPermissionGroupsAspNetUsers_RESTQuery : IRequest<Response<List<PermissionGroupsAspNetUsers_RESTResponseDto>>>
    {
        public int PermissionGroupsId { get; set; }

        public PermissionGroupsPermissionGroupsAspNetUsers_RESTQuery(int permissionGroupsId)
        {
            this.PermissionGroupsId = permissionGroupsId;
        }

        public class PermissionGroupsPermissionGroupsAspNetUsers_RESTHandler : IRequestHandler<PermissionGroupsPermissionGroupsAspNetUsers_RESTQuery, Response<List<PermissionGroupsAspNetUsers_RESTResponseDto>>>
        {
            private readonly ILogger<PermissionGroupsPermissionGroupsAspNetUsers_RESTHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsPermissionGroupsAspNetUsers_RESTHandler(ILogger<PermissionGroupsPermissionGroupsAspNetUsers_RESTHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PermissionGroupsAspNetUsers_RESTResponseDto>>> Handle(PermissionGroupsPermissionGroupsAspNetUsers_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PermissionGroupsAspNetUsers_RESTAsync(request.PermissionGroupsId);
                return returnValue.ToResponse();
            }
        }
    }
}