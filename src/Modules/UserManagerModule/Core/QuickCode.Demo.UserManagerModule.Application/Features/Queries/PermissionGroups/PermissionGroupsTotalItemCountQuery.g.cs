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
    public class PermissionGroupsTotalItemCountQuery : IRequest<Response<int>>
    {
        public PermissionGroupsTotalItemCountQuery()
        {
        }

        public class PermissionGroupsTotalItemCountHandler : IRequestHandler<PermissionGroupsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<PermissionGroupsTotalItemCountHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsTotalItemCountHandler(ILogger<PermissionGroupsTotalItemCountHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(PermissionGroupsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}