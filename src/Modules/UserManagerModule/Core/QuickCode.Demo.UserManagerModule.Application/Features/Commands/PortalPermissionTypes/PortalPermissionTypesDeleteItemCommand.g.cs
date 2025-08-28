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
    public class PortalPermissionTypesDeleteItemCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public PortalPermissionTypesDeleteItemCommand(int id)
        {
            this.Id = id;
        }

        public class PortalPermissionTypesDeleteItemHandler : IRequestHandler<PortalPermissionTypesDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<PortalPermissionTypesDeleteItemHandler> _logger;
            private readonly IPortalPermissionTypesRepository _repository;
            public PortalPermissionTypesDeleteItemHandler(ILogger<PortalPermissionTypesDeleteItemHandler> logger, IPortalPermissionTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PortalPermissionTypesDeleteItemCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Id);
                if (deleteItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}