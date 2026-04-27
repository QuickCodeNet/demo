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
using QuickCode.Demo.IdentityModule.Application.Dtos.ApiMethodDefinition;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.ApiMethodDefinition
{
    public class DeleteApiMethodDefinitionsWithModuleNameCommand : IRequest<Response<int>>
    {
        public string ApiMethodDefinitionModuleName { get; set; }

        public DeleteApiMethodDefinitionsWithModuleNameCommand(string apiMethodDefinitionModuleName)
        {
            this.ApiMethodDefinitionModuleName = apiMethodDefinitionModuleName;
        }

        public class DeleteApiMethodDefinitionsWithModuleNameHandler : IRequestHandler<DeleteApiMethodDefinitionsWithModuleNameCommand, Response<int>>
        {
            private readonly ILogger<DeleteApiMethodDefinitionsWithModuleNameHandler> _logger;
            private readonly IApiMethodDefinitionRepository _repository;
            public DeleteApiMethodDefinitionsWithModuleNameHandler(ILogger<DeleteApiMethodDefinitionsWithModuleNameHandler> logger, IApiMethodDefinitionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(DeleteApiMethodDefinitionsWithModuleNameCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.DeleteApiMethodDefinitionsWithModuleNameAsync(request.ApiMethodDefinitionModuleName);
                return returnValue.ToResponse();
            }
        }
    }
}