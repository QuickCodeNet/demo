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
    public class PortalPermissionGroupsUpdateCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public PortalPermissionGroupsDto request { get; set; }

        public PortalPermissionGroupsUpdateCommand(int id, PortalPermissionGroupsDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class PortalPermissionGroupsUpdateHandler : IRequestHandler<PortalPermissionGroupsUpdateCommand, Response<bool>>
        {
            private readonly ILogger<PortalPermissionGroupsUpdateHandler> _logger;
            private readonly IPortalPermissionGroupsRepository _repository;
            public PortalPermissionGroupsUpdateHandler(ILogger<PortalPermissionGroupsUpdateHandler> logger, IPortalPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PortalPermissionGroupsUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
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