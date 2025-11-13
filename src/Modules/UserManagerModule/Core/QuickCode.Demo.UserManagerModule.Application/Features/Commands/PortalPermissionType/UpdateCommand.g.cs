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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionType;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionType
{
    public class UpdatePortalPermissionTypeCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public PortalPermissionTypeDto request { get; set; }

        public UpdatePortalPermissionTypeCommand(int id, PortalPermissionTypeDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class UpdatePortalPermissionTypeHandler : IRequestHandler<UpdatePortalPermissionTypeCommand, Response<bool>>
        {
            private readonly ILogger<UpdatePortalPermissionTypeHandler> _logger;
            private readonly IPortalPermissionTypeRepository _repository;
            public UpdatePortalPermissionTypeHandler(ILogger<UpdatePortalPermissionTypeHandler> logger, IPortalPermissionTypeRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdatePortalPermissionTypeCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}