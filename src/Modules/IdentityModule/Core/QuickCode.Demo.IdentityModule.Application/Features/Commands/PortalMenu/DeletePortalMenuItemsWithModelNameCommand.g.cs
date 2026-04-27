using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.IdentityModule.Domain.Entities;
using QuickCode.Demo.IdentityModule.Application.Interfaces.Repositories;
using QuickCode.Demo.IdentityModule.Application.Dtos.PortalMenu;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.PortalMenu
{
    public class DeletePortalMenuItemsWithModelNameCommand : IRequest<Response<int>>
    {
        public string PortalMenuKey { get; set; }
        public string PortalMenuName { get; set; }

        public DeletePortalMenuItemsWithModelNameCommand(string portalMenuKey, string portalMenuName)
        {
            this.PortalMenuKey = portalMenuKey;
            this.PortalMenuName = portalMenuName;
        }

        public class DeletePortalMenuItemsWithModelNameHandler : IRequestHandler<DeletePortalMenuItemsWithModelNameCommand, Response<int>>
        {
            private readonly ILogger<DeletePortalMenuItemsWithModelNameHandler> _logger;
            private readonly IPortalMenuRepository _repository;
            public DeletePortalMenuItemsWithModelNameHandler(ILogger<DeletePortalMenuItemsWithModelNameHandler> logger, IPortalMenuRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(DeletePortalMenuItemsWithModelNameCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.DeletePortalMenuItemsWithModelNameAsync(request.PortalMenuKey, request.PortalMenuName);
                return returnValue.ToResponse();
            }
        }
    }
}