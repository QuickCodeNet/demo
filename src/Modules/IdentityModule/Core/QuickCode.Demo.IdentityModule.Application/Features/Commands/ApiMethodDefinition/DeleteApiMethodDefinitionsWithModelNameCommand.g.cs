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
    public class DeleteApiMethodDefinitionsWithModelNameCommand : IRequest<Response<int>>
    {
        public string ApiMethodDefinitionModelName { get; set; }

        public DeleteApiMethodDefinitionsWithModelNameCommand(string apiMethodDefinitionModelName)
        {
            this.ApiMethodDefinitionModelName = apiMethodDefinitionModelName;
        }

        public class DeleteApiMethodDefinitionsWithModelNameHandler : IRequestHandler<DeleteApiMethodDefinitionsWithModelNameCommand, Response<int>>
        {
            private readonly ILogger<DeleteApiMethodDefinitionsWithModelNameHandler> _logger;
            private readonly IApiMethodDefinitionRepository _repository;
            public DeleteApiMethodDefinitionsWithModelNameHandler(ILogger<DeleteApiMethodDefinitionsWithModelNameHandler> logger, IApiMethodDefinitionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(DeleteApiMethodDefinitionsWithModelNameCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.DeleteApiMethodDefinitionsWithModelNameAsync(request.ApiMethodDefinitionModelName);
                return returnValue.ToResponse();
            }
        }
    }
}