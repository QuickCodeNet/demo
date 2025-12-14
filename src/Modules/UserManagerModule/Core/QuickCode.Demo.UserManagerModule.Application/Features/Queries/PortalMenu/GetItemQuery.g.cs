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
    public class GetItemPortalMenuQuery : IRequest<Response<PortalMenuDto>>
    {
        public string Key { get; set; }

        public GetItemPortalMenuQuery(string key)
        {
            this.Key = key;
        }

        public class GetItemPortalMenuHandler : IRequestHandler<GetItemPortalMenuQuery, Response<PortalMenuDto>>
        {
            private readonly ILogger<GetItemPortalMenuHandler> _logger;
            private readonly IPortalMenuRepository _repository;
            public GetItemPortalMenuHandler(ILogger<GetItemPortalMenuHandler> logger, IPortalMenuRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalMenuDto>> Handle(GetItemPortalMenuQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Key);
                return returnValue.ToResponse();
            }
        }
    }
}