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
    public class ApiPermissionGroupsTotalItemCountQuery : IRequest<Response<int>>
    {
        public ApiPermissionGroupsTotalItemCountQuery()
        {
        }

        public class ApiPermissionGroupsTotalItemCountHandler : IRequestHandler<ApiPermissionGroupsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<ApiPermissionGroupsTotalItemCountHandler> _logger;
            private readonly IApiPermissionGroupsRepository _repository;
            public ApiPermissionGroupsTotalItemCountHandler(ILogger<ApiPermissionGroupsTotalItemCountHandler> logger, IApiPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(ApiPermissionGroupsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}