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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PermissionGroup;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PermissionGroup
{
    public class UpdatePermissionGroupCommand : IRequest<Response<bool>>
    {
        public string Name { get; set; }
        public PermissionGroupDto request { get; set; }

        public UpdatePermissionGroupCommand(string name, PermissionGroupDto request)
        {
            this.request = request;
            this.Name = name;
        }

        public class UpdatePermissionGroupHandler : IRequestHandler<UpdatePermissionGroupCommand, Response<bool>>
        {
            private readonly ILogger<UpdatePermissionGroupHandler> _logger;
            private readonly IPermissionGroupRepository _repository;
            public UpdatePermissionGroupHandler(ILogger<UpdatePermissionGroupHandler> logger, IPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdatePermissionGroupCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Name);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}