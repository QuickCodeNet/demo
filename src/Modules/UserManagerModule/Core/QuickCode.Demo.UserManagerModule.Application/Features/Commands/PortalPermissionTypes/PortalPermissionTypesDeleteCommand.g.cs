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
    public class PortalPermissionTypesDeleteCommand : IRequest<Response<bool>>
    {
        public PortalPermissionTypesDto request { get; set; }

        public PortalPermissionTypesDeleteCommand(PortalPermissionTypesDto request)
        {
            this.request = request;
        }

        public class PortalPermissionTypesDeleteHandler : IRequestHandler<PortalPermissionTypesDeleteCommand, Response<bool>>
        {
            private readonly ILogger<PortalPermissionTypesDeleteHandler> _logger;
            private readonly IPortalPermissionTypesRepository _repository;
            public PortalPermissionTypesDeleteHandler(ILogger<PortalPermissionTypesDeleteHandler> logger, IPortalPermissionTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PortalPermissionTypesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}