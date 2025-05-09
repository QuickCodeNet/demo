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
    public class PermissionGroupsListQuery : IRequest<Response<List<PermissionGroupsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public PermissionGroupsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class PermissionGroupsListHandler : IRequestHandler<PermissionGroupsListQuery, Response<List<PermissionGroupsDto>>>
        {
            private readonly ILogger<PermissionGroupsListHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsListHandler(ILogger<PermissionGroupsListHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PermissionGroupsDto>>> Handle(PermissionGroupsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}