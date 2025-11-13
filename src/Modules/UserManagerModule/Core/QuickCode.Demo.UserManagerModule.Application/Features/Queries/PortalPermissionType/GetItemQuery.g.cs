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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionType;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionType
{
    public class GetItemPortalPermissionTypeQuery : IRequest<Response<PortalPermissionTypeDto>>
    {
        public int Id { get; set; }

        public GetItemPortalPermissionTypeQuery(int id)
        {
            this.Id = id;
        }

        public class GetItemPortalPermissionTypeHandler : IRequestHandler<GetItemPortalPermissionTypeQuery, Response<PortalPermissionTypeDto>>
        {
            private readonly ILogger<GetItemPortalPermissionTypeHandler> _logger;
            private readonly IPortalPermissionTypeRepository _repository;
            public GetItemPortalPermissionTypeHandler(ILogger<GetItemPortalPermissionTypeHandler> logger, IPortalPermissionTypeRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionTypeDto>> Handle(GetItemPortalPermissionTypeQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}