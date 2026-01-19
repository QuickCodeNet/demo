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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalMenu;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalMenu
{
    public class DeletePortalMenuItemsWithModuleNameCommand : IRequest<Response<int>>
    {
        public string PortalMenusKey { get; set; }

        public DeletePortalMenuItemsWithModuleNameCommand(string portalMenusKey)
        {
            this.PortalMenusKey = portalMenusKey;
        }

        public class DeletePortalMenuItemsWithModuleNameHandler : IRequestHandler<DeletePortalMenuItemsWithModuleNameCommand, Response<int>>
        {
            private readonly ILogger<DeletePortalMenuItemsWithModuleNameHandler> _logger;
            private readonly IPortalMenuRepository _repository;
            public DeletePortalMenuItemsWithModuleNameHandler(ILogger<DeletePortalMenuItemsWithModuleNameHandler> logger, IPortalMenuRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(DeletePortalMenuItemsWithModuleNameCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.DeletePortalMenuItemsWithModuleNameAsync(request.PortalMenusKey);
                return returnValue.ToResponse();
            }
        }
    }
}