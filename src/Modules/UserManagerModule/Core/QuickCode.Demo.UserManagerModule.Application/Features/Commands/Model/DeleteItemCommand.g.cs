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
using QuickCode.Demo.UserManagerModule.Application.Dtos.Model;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.Model
{
    public class DeleteItemModelCommand : IRequest<Response<bool>>
    {
        public string Name { get; set; }
        public string ModuleName { get; set; }

        public DeleteItemModelCommand(string name, string moduleName)
        {
            this.Name = name;
            this.ModuleName = moduleName;
        }

        public class DeleteItemModelHandler : IRequestHandler<DeleteItemModelCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemModelHandler> _logger;
            private readonly IModelRepository _repository;
            public DeleteItemModelHandler(ILogger<DeleteItemModelHandler> logger, IModelRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemModelCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Name, request.ModuleName);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}