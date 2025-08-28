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
    public class ApiPermissionGroupsInsertCommand : IRequest<Response<ApiPermissionGroupsDto>>
    {
        public ApiPermissionGroupsDto request { get; set; }

        public ApiPermissionGroupsInsertCommand(ApiPermissionGroupsDto request)
        {
            this.request = request;
        }

        public class ApiPermissionGroupsInsertHandler : IRequestHandler<ApiPermissionGroupsInsertCommand, Response<ApiPermissionGroupsDto>>
        {
            private readonly ILogger<ApiPermissionGroupsInsertHandler> _logger;
            private readonly IApiPermissionGroupsRepository _repository;
            public ApiPermissionGroupsInsertHandler(ILogger<ApiPermissionGroupsInsertHandler> logger, IApiPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApiPermissionGroupsDto>> Handle(ApiPermissionGroupsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}