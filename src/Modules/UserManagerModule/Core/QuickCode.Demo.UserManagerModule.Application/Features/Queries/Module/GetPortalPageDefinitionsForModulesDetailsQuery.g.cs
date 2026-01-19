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
using QuickCode.Demo.UserManagerModule.Application.Dtos.Module;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.Module
{
    public class GetPortalPageDefinitionsForModulesDetailsQuery : IRequest<Response<GetPortalPageDefinitionsForModulesResponseDto>>
    {
        public string ModulesName { get; set; }
        public string PortalPageDefinitionsKey { get; set; }

        public GetPortalPageDefinitionsForModulesDetailsQuery(string modulesName, string portalPageDefinitionsKey)
        {
            this.ModulesName = modulesName;
            this.PortalPageDefinitionsKey = portalPageDefinitionsKey;
        }

        public class GetPortalPageDefinitionsForModulesDetailsHandler : IRequestHandler<GetPortalPageDefinitionsForModulesDetailsQuery, Response<GetPortalPageDefinitionsForModulesResponseDto>>
        {
            private readonly ILogger<GetPortalPageDefinitionsForModulesDetailsHandler> _logger;
            private readonly IModuleRepository _repository;
            public GetPortalPageDefinitionsForModulesDetailsHandler(ILogger<GetPortalPageDefinitionsForModulesDetailsHandler> logger, IModuleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetPortalPageDefinitionsForModulesResponseDto>> Handle(GetPortalPageDefinitionsForModulesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetPortalPageDefinitionsForModulesDetailsAsync(request.ModulesName, request.PortalPageDefinitionsKey);
                return returnValue.ToResponse();
            }
        }
    }
}