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
    public class DeleteModelCommand : IRequest<Response<bool>>
    {
        public ModelDto request { get; set; }

        public DeleteModelCommand(ModelDto request)
        {
            this.request = request;
        }

        public class DeleteModelHandler : IRequestHandler<DeleteModelCommand, Response<bool>>
        {
            private readonly ILogger<DeleteModelHandler> _logger;
            private readonly IModelRepository _repository;
            public DeleteModelHandler(ILogger<DeleteModelHandler> logger, IModelRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}