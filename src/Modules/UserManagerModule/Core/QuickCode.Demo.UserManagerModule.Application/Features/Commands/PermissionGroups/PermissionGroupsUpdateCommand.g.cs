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
    public class PermissionGroupsUpdateCommand : IRequest<Response<bool>>
    {
        public string Name { get; set; }
        public PermissionGroupsDto request { get; set; }

        public PermissionGroupsUpdateCommand(string name, PermissionGroupsDto request)
        {
            this.request = request;
            this.Name = name;
        }

        public class PermissionGroupsUpdateHandler : IRequestHandler<PermissionGroupsUpdateCommand, Response<bool>>
        {
            private readonly ILogger<PermissionGroupsUpdateHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsUpdateHandler(ILogger<PermissionGroupsUpdateHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PermissionGroupsUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Name);
                if (updateItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}