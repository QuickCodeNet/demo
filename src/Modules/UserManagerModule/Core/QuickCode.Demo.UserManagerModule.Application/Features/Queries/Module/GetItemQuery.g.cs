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
    public class GetItemModuleQuery : IRequest<Response<ModuleDto>>
    {
        public string Name { get; set; }

        public GetItemModuleQuery(string name)
        {
            this.Name = name;
        }

        public class GetItemModuleHandler : IRequestHandler<GetItemModuleQuery, Response<ModuleDto>>
        {
            private readonly ILogger<GetItemModuleHandler> _logger;
            private readonly IModuleRepository _repository;
            public GetItemModuleHandler(ILogger<GetItemModuleHandler> logger, IModuleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ModuleDto>> Handle(GetItemModuleQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Name);
                return returnValue.ToResponse();
            }
        }
    }
}