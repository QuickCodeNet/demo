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
    public class PortalPermissionsInsertCommand : IRequest<Response<PortalPermissionsDto>>
    {
        public PortalPermissionsDto request { get; set; }

        public PortalPermissionsInsertCommand(PortalPermissionsDto request)
        {
            this.request = request;
        }

        public class PortalPermissionsInsertHandler : IRequestHandler<PortalPermissionsInsertCommand, Response<PortalPermissionsDto>>
        {
            private readonly ILogger<PortalPermissionsInsertHandler> _logger;
            private readonly IPortalPermissionsRepository _repository;
            public PortalPermissionsInsertHandler(ILogger<PortalPermissionsInsertHandler> logger, IPortalPermissionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionsDto>> Handle(PortalPermissionsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}