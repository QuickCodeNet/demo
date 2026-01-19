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
    public class GetPortalPageDefinitionsForModulesQuery : IRequest<Response<List<GetPortalPageDefinitionsForModulesResponseDto>>>
    {
        public string ModulesName { get; set; }

        public GetPortalPageDefinitionsForModulesQuery(string modulesName)
        {
            this.ModulesName = modulesName;
        }

        public class GetPortalPageDefinitionsForModulesHandler : IRequestHandler<GetPortalPageDefinitionsForModulesQuery, Response<List<GetPortalPageDefinitionsForModulesResponseDto>>>
        {
            private readonly ILogger<GetPortalPageDefinitionsForModulesHandler> _logger;
            private readonly IModuleRepository _repository;
            public GetPortalPageDefinitionsForModulesHandler(ILogger<GetPortalPageDefinitionsForModulesHandler> logger, IModuleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetPortalPageDefinitionsForModulesResponseDto>>> Handle(GetPortalPageDefinitionsForModulesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetPortalPageDefinitionsForModulesAsync(request.ModulesName);
                return returnValue.ToResponse();
            }
        }
    }
}