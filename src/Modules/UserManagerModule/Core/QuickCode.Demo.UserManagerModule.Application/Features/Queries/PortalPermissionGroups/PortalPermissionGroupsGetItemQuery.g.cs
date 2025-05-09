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
    public class PortalPermissionGroupsGetItemQuery : IRequest<Response<PortalPermissionGroupsDto>>
    {
        public int Id { get; set; }

        public PortalPermissionGroupsGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class PortalPermissionGroupsGetItemHandler : IRequestHandler<PortalPermissionGroupsGetItemQuery, Response<PortalPermissionGroupsDto>>
        {
            private readonly ILogger<PortalPermissionGroupsGetItemHandler> _logger;
            private readonly IPortalPermissionGroupsRepository _repository;
            public PortalPermissionGroupsGetItemHandler(ILogger<PortalPermissionGroupsGetItemHandler> logger, IPortalPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionGroupsDto>> Handle(PortalPermissionGroupsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}