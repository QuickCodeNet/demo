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
    public class InsertModelCommand : IRequest<Response<ModelDto>>
    {
        public ModelDto request { get; set; }

        public InsertModelCommand(ModelDto request)
        {
            this.request = request;
        }

        public class InsertModelHandler : IRequestHandler<InsertModelCommand, Response<ModelDto>>
        {
            private readonly ILogger<InsertModelHandler> _logger;
            private readonly IModelRepository _repository;
            public InsertModelHandler(ILogger<InsertModelHandler> logger, IModelRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ModelDto>> Handle(InsertModelCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}