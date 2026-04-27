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
using QuickCode.Demo.IdentityModule.Application.Dtos.Module;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.Module
{
    public class ModuleNameIsExistsQuery : IRequest<Response<bool>>
    {
        public string ModuleName { get; set; }

        public ModuleNameIsExistsQuery(string moduleName)
        {
            this.ModuleName = moduleName;
        }

        public class ModuleNameIsExistsHandler : IRequestHandler<ModuleNameIsExistsQuery, Response<bool>>
        {
            private readonly ILogger<ModuleNameIsExistsHandler> _logger;
            private readonly IModuleRepository _repository;
            public ModuleNameIsExistsHandler(ILogger<ModuleNameIsExistsHandler> logger, IModuleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ModuleNameIsExistsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ModuleNameIsExistsAsync(request.ModuleName);
                return returnValue.ToResponse();
            }
        }
    }
}