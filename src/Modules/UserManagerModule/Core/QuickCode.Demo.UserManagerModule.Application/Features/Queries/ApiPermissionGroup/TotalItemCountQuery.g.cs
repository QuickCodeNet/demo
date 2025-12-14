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
using QuickCode.Demo.UserManagerModule.Application.Dtos.ApiPermissionGroup;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.ApiPermissionGroup
{
    public class TotalCountApiPermissionGroupQuery : IRequest<Response<int>>
    {
        public TotalCountApiPermissionGroupQuery()
        {
        }

        public class TotalCountApiPermissionGroupHandler : IRequestHandler<TotalCountApiPermissionGroupQuery, Response<int>>
        {
            private readonly ILogger<TotalCountApiPermissionGroupHandler> _logger;
            private readonly IApiPermissionGroupRepository _repository;
            public TotalCountApiPermissionGroupHandler(ILogger<TotalCountApiPermissionGroupHandler> logger, IApiPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountApiPermissionGroupQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}