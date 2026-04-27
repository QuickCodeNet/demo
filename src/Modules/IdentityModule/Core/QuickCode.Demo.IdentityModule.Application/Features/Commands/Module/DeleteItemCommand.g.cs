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
    public class DeleteItemModuleCommand : IRequest<Response<bool>>
    {
        public string Name { get; set; }

        public DeleteItemModuleCommand(string name)
        {
            this.Name = name;
        }

        public class DeleteItemModuleHandler : IRequestHandler<DeleteItemModuleCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemModuleHandler> _logger;
            private readonly IModuleRepository _repository;
            public DeleteItemModuleHandler(ILogger<DeleteItemModuleHandler> logger, IModuleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemModuleCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Name);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}