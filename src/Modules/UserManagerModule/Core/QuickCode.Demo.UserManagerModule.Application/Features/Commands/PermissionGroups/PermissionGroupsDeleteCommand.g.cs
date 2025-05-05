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
    public class PermissionGroupsDeleteCommand : IRequest<Response<bool>>
    {
        public PermissionGroupsDto request { get; set; }

        public PermissionGroupsDeleteCommand(PermissionGroupsDto request)
        {
            this.request = request;
        }

        public class PermissionGroupsDeleteHandler : IRequestHandler<PermissionGroupsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<PermissionGroupsDeleteHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsDeleteHandler(ILogger<PermissionGroupsDeleteHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PermissionGroupsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}