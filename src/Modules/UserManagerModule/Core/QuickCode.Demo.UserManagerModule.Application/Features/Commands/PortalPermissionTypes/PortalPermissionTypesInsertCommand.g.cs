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
    public class PortalPermissionTypesInsertCommand : IRequest<Response<PortalPermissionTypesDto>>
    {
        public PortalPermissionTypesDto request { get; set; }

        public PortalPermissionTypesInsertCommand(PortalPermissionTypesDto request)
        {
            this.request = request;
        }

        public class PortalPermissionTypesInsertHandler : IRequestHandler<PortalPermissionTypesInsertCommand, Response<PortalPermissionTypesDto>>
        {
            private readonly ILogger<PortalPermissionTypesInsertHandler> _logger;
            private readonly IPortalPermissionTypesRepository _repository;
            public PortalPermissionTypesInsertHandler(ILogger<PortalPermissionTypesInsertHandler> logger, IPortalPermissionTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionTypesDto>> Handle(PortalPermissionTypesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}