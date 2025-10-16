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
    public class InsertPortalPermissionTypeCommand : IRequest<Response<PortalPermissionTypeDto>>
    {
        public PortalPermissionTypeDto request { get; set; }

        public InsertPortalPermissionTypeCommand(PortalPermissionTypeDto request)
        {
            this.request = request;
        }

        public class InsertPortalPermissionTypeHandler : IRequestHandler<InsertPortalPermissionTypeCommand, Response<PortalPermissionTypeDto>>
        {
            private readonly ILogger<InsertPortalPermissionTypeHandler> _logger;
            private readonly IPortalPermissionTypeRepository _repository;
            public InsertPortalPermissionTypeHandler(ILogger<InsertPortalPermissionTypeHandler> logger, IPortalPermissionTypeRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionTypeDto>> Handle(InsertPortalPermissionTypeCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}