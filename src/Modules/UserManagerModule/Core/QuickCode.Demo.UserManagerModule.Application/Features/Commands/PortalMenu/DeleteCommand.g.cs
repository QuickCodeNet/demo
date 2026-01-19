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
    public class DeletePortalMenuCommand : IRequest<Response<bool>>
    {
        public PortalMenuDto request { get; set; }

        public DeletePortalMenuCommand(PortalMenuDto request)
        {
            this.request = request;
        }

        public class DeletePortalMenuHandler : IRequestHandler<DeletePortalMenuCommand, Response<bool>>
        {
            private readonly ILogger<DeletePortalMenuHandler> _logger;
            private readonly IPortalMenuRepository _repository;
            public DeletePortalMenuHandler(ILogger<DeletePortalMenuHandler> logger, IPortalMenuRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeletePortalMenuCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}