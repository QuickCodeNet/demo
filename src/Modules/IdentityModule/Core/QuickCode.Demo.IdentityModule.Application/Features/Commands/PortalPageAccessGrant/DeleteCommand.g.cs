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
    public class DeletePortalPageAccessGrantCommand : IRequest<Response<bool>>
    {
        public PortalPageAccessGrantDto request { get; set; }

        public DeletePortalPageAccessGrantCommand(PortalPageAccessGrantDto request)
        {
            this.request = request;
        }

        public class DeletePortalPageAccessGrantHandler : IRequestHandler<DeletePortalPageAccessGrantCommand, Response<bool>>
        {
            private readonly ILogger<DeletePortalPageAccessGrantHandler> _logger;
            private readonly IPortalPageAccessGrantRepository _repository;
            public DeletePortalPageAccessGrantHandler(ILogger<DeletePortalPageAccessGrantHandler> logger, IPortalPageAccessGrantRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeletePortalPageAccessGrantCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}