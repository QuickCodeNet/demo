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
    public class DeleteItemPortalPermissionTypeCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public DeleteItemPortalPermissionTypeCommand(int id)
        {
            this.Id = id;
        }

        public class DeleteItemPortalPermissionTypeHandler : IRequestHandler<DeleteItemPortalPermissionTypeCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemPortalPermissionTypeHandler> _logger;
            private readonly IPortalPermissionTypeRepository _repository;
            public DeleteItemPortalPermissionTypeHandler(ILogger<DeleteItemPortalPermissionTypeHandler> logger, IPortalPermissionTypeRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemPortalPermissionTypeCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Id);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}