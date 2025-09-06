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
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class PortalPermissionsGetItemQuery : IRequest<Response<PortalPermissionsDto>>
    {
        public string Name { get; set; }

        public PortalPermissionsGetItemQuery(string name)
        {
            this.Name = name;
        }

        public class PortalPermissionsGetItemHandler : IRequestHandler<PortalPermissionsGetItemQuery, Response<PortalPermissionsDto>>
        {
            private readonly ILogger<PortalPermissionsGetItemHandler> _logger;
            private readonly IPortalPermissionsRepository _repository;
            public PortalPermissionsGetItemHandler(ILogger<PortalPermissionsGetItemHandler> logger, IPortalPermissionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionsDto>> Handle(PortalPermissionsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Name);
                return returnValue.ToResponse();
            }
        }
    }
}