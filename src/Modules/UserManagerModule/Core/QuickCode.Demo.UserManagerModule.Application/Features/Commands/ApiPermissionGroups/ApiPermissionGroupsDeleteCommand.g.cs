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
    public class ApiPermissionGroupsDeleteCommand : IRequest<Response<bool>>
    {
        public ApiPermissionGroupsDto request { get; set; }

        public ApiPermissionGroupsDeleteCommand(ApiPermissionGroupsDto request)
        {
            this.request = request;
        }

        public class ApiPermissionGroupsDeleteHandler : IRequestHandler<ApiPermissionGroupsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<ApiPermissionGroupsDeleteHandler> _logger;
            private readonly IApiPermissionGroupsRepository _repository;
            public ApiPermissionGroupsDeleteHandler(ILogger<ApiPermissionGroupsDeleteHandler> logger, IApiPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ApiPermissionGroupsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}