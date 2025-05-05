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
    public class PortalPermissionGroupsInsertCommand : IRequest<Response<PortalPermissionGroupsDto>>
    {
        public PortalPermissionGroupsDto request { get; set; }

        public PortalPermissionGroupsInsertCommand(PortalPermissionGroupsDto request)
        {
            this.request = request;
        }

        public class PortalPermissionGroupsInsertHandler : IRequestHandler<PortalPermissionGroupsInsertCommand, Response<PortalPermissionGroupsDto>>
        {
            private readonly ILogger<PortalPermissionGroupsInsertHandler> _logger;
            private readonly IPortalPermissionGroupsRepository _repository;
            public PortalPermissionGroupsInsertHandler(ILogger<PortalPermissionGroupsInsertHandler> logger, IPortalPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionGroupsDto>> Handle(PortalPermissionGroupsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}