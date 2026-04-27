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
using QuickCode.Demo.IdentityModule.Application.Dtos.PortalPageAccessGrant;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.PortalPageAccessGrant
{
    public class ClearPortalPageAccessGrantsCommand : IRequest<Response<int>>
    {
        public ClearPortalPageAccessGrantsCommand()
        {
        }

        public class ClearPortalPageAccessGrantsHandler : IRequestHandler<ClearPortalPageAccessGrantsCommand, Response<int>>
        {
            private readonly ILogger<ClearPortalPageAccessGrantsHandler> _logger;
            private readonly IPortalPageAccessGrantRepository _repository;
            public ClearPortalPageAccessGrantsHandler(ILogger<ClearPortalPageAccessGrantsHandler> logger, IPortalPageAccessGrantRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(ClearPortalPageAccessGrantsCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ClearPortalPageAccessGrantsAsync();
                return returnValue.ToResponse();
            }
        }
    }
}