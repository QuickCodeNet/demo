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
    public class PermissionGroupsGetItemQuery : IRequest<Response<PermissionGroupsDto>>
    {
        public int Id { get; set; }

        public PermissionGroupsGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class PermissionGroupsGetItemHandler : IRequestHandler<PermissionGroupsGetItemQuery, Response<PermissionGroupsDto>>
        {
            private readonly ILogger<PermissionGroupsGetItemHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsGetItemHandler(ILogger<PermissionGroupsGetItemHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PermissionGroupsDto>> Handle(PermissionGroupsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}