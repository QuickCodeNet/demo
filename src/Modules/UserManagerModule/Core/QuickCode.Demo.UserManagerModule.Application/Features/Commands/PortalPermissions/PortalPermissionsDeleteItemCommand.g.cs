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
    public class PortalPermissionsDeleteItemCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public PortalPermissionsDeleteItemCommand(int id)
        {
            this.Id = id;
        }

        public class PortalPermissionsDeleteItemHandler : IRequestHandler<PortalPermissionsDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<PortalPermissionsDeleteItemHandler> _logger;
            private readonly IPortalPermissionsRepository _repository;
            public PortalPermissionsDeleteItemHandler(ILogger<PortalPermissionsDeleteItemHandler> logger, IPortalPermissionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PortalPermissionsDeleteItemCommand request, CancellationToken cancellationToken)
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