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
    public class PortalPermissionTypesGetItemQuery : IRequest<Response<PortalPermissionTypesDto>>
    {
        public int Id { get; set; }

        public PortalPermissionTypesGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class PortalPermissionTypesGetItemHandler : IRequestHandler<PortalPermissionTypesGetItemQuery, Response<PortalPermissionTypesDto>>
        {
            private readonly ILogger<PortalPermissionTypesGetItemHandler> _logger;
            private readonly IPortalPermissionTypesRepository _repository;
            public PortalPermissionTypesGetItemHandler(ILogger<PortalPermissionTypesGetItemHandler> logger, IPortalPermissionTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionTypesDto>> Handle(PortalPermissionTypesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}