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
    public class PortalPermissionsDeleteCommand : IRequest<Response<bool>>
    {
        public PortalPermissionsDto request { get; set; }

        public PortalPermissionsDeleteCommand(PortalPermissionsDto request)
        {
            this.request = request;
        }

        public class PortalPermissionsDeleteHandler : IRequestHandler<PortalPermissionsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<PortalPermissionsDeleteHandler> _logger;
            private readonly IPortalPermissionsRepository _repository;
            public PortalPermissionsDeleteHandler(ILogger<PortalPermissionsDeleteHandler> logger, IPortalPermissionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PortalPermissionsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}