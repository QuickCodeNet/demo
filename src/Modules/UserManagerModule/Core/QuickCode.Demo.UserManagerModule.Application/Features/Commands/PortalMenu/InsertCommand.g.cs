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
    public class InsertPortalMenuCommand : IRequest<Response<PortalMenuDto>>
    {
        public PortalMenuDto request { get; set; }

        public InsertPortalMenuCommand(PortalMenuDto request)
        {
            this.request = request;
        }

        public class InsertPortalMenuHandler : IRequestHandler<InsertPortalMenuCommand, Response<PortalMenuDto>>
        {
            private readonly ILogger<InsertPortalMenuHandler> _logger;
            private readonly IPortalMenuRepository _repository;
            public InsertPortalMenuHandler(ILogger<InsertPortalMenuHandler> logger, IPortalMenuRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalMenuDto>> Handle(InsertPortalMenuCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}