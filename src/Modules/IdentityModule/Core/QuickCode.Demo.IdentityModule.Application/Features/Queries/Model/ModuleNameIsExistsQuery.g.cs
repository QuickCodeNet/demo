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
using QuickCode.Demo.IdentityModule.Application.Dtos.Model;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.Model
{
    public class ModuleNameIsExistsQuery : IRequest<Response<bool>>
    {
        public string ModelModuleName { get; set; }

        public ModuleNameIsExistsQuery(string modelModuleName)
        {
            this.ModelModuleName = modelModuleName;
        }

        public class ModuleNameIsExistsHandler : IRequestHandler<ModuleNameIsExistsQuery, Response<bool>>
        {
            private readonly ILogger<ModuleNameIsExistsHandler> _logger;
            private readonly IModelRepository _repository;
            public ModuleNameIsExistsHandler(ILogger<ModuleNameIsExistsHandler> logger, IModelRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ModuleNameIsExistsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ModuleNameIsExistsAsync(request.ModelModuleName);
                return returnValue.ToResponse();
            }
        }
    }
}