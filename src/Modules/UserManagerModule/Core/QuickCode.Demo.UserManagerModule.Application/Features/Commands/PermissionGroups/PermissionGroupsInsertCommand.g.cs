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
    public class PermissionGroupsInsertCommand : IRequest<Response<PermissionGroupsDto>>
    {
        public PermissionGroupsDto request { get; set; }

        public PermissionGroupsInsertCommand(PermissionGroupsDto request)
        {
            this.request = request;
        }

        public class PermissionGroupsInsertHandler : IRequestHandler<PermissionGroupsInsertCommand, Response<PermissionGroupsDto>>
        {
            private readonly ILogger<PermissionGroupsInsertHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsInsertHandler(ILogger<PermissionGroupsInsertHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PermissionGroupsDto>> Handle(PermissionGroupsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}