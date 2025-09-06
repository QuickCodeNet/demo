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
    public class PortalPermissionsListQuery : IRequest<Response<List<PortalPermissionsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public PortalPermissionsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class PortalPermissionsListHandler : IRequestHandler<PortalPermissionsListQuery, Response<List<PortalPermissionsDto>>>
        {
            private readonly ILogger<PortalPermissionsListHandler> _logger;
            private readonly IPortalPermissionsRepository _repository;
            public PortalPermissionsListHandler(ILogger<PortalPermissionsListHandler> logger, IPortalPermissionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPermissionsDto>>> Handle(PortalPermissionsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}