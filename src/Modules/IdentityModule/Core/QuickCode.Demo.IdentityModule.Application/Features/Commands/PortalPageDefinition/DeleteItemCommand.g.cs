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
    public class DeleteItemPortalPageDefinitionCommand : IRequest<Response<bool>>
    {
        public string Key { get; set; }

        public DeleteItemPortalPageDefinitionCommand(string key)
        {
            this.Key = key;
        }

        public class DeleteItemPortalPageDefinitionHandler : IRequestHandler<DeleteItemPortalPageDefinitionCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemPortalPageDefinitionHandler> _logger;
            private readonly IPortalPageDefinitionRepository _repository;
            public DeleteItemPortalPageDefinitionHandler(ILogger<DeleteItemPortalPageDefinitionHandler> logger, IPortalPageDefinitionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemPortalPageDefinitionCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Key);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}