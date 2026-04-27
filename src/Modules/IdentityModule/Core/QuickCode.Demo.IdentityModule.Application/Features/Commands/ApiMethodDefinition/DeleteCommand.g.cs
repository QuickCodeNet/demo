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
    public class DeleteApiMethodDefinitionCommand : IRequest<Response<bool>>
    {
        public ApiMethodDefinitionDto request { get; set; }

        public DeleteApiMethodDefinitionCommand(ApiMethodDefinitionDto request)
        {
            this.request = request;
        }

        public class DeleteApiMethodDefinitionHandler : IRequestHandler<DeleteApiMethodDefinitionCommand, Response<bool>>
        {
            private readonly ILogger<DeleteApiMethodDefinitionHandler> _logger;
            private readonly IApiMethodDefinitionRepository _repository;
            public DeleteApiMethodDefinitionHandler(ILogger<DeleteApiMethodDefinitionHandler> logger, IApiMethodDefinitionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteApiMethodDefinitionCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}