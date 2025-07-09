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
    public class PortalPermissionTypesListQuery : IRequest<Response<List<PortalPermissionTypesDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public PortalPermissionTypesListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class PortalPermissionTypesListHandler : IRequestHandler<PortalPermissionTypesListQuery, Response<List<PortalPermissionTypesDto>>>
        {
            private readonly ILogger<PortalPermissionTypesListHandler> _logger;
            private readonly IPortalPermissionTypesRepository _repository;
            public PortalPermissionTypesListHandler(ILogger<PortalPermissionTypesListHandler> logger, IPortalPermissionTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPermissionTypesDto>>> Handle(PortalPermissionTypesListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}