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
using QuickCode.Demo.IdentityModule.Application.Dtos.PortalPageDefinition;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.PortalPageDefinition
{
    public class InsertPortalPageDefinitionCommand : IRequest<Response<PortalPageDefinitionDto>>
    {
        public PortalPageDefinitionDto request { get; set; }

        public InsertPortalPageDefinitionCommand(PortalPageDefinitionDto request)
        {
            this.request = request;
        }

        public class InsertPortalPageDefinitionHandler : IRequestHandler<InsertPortalPageDefinitionCommand, Response<PortalPageDefinitionDto>>
        {
            private readonly ILogger<InsertPortalPageDefinitionHandler> _logger;
            private readonly IPortalPageDefinitionRepository _repository;
            public InsertPortalPageDefinitionHandler(ILogger<InsertPortalPageDefinitionHandler> logger, IPortalPageDefinitionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPageDefinitionDto>> Handle(InsertPortalPageDefinitionCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}