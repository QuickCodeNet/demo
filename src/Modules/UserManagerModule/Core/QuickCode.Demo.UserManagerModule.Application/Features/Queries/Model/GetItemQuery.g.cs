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
    public class GetItemModelQuery : IRequest<Response<ModelDto>>
    {
        public string Name { get; set; }
        public string ModuleName { get; set; }

        public GetItemModelQuery(string name, string moduleName)
        {
            this.Name = name;
            this.ModuleName = moduleName;
        }

        public class GetItemModelHandler : IRequestHandler<GetItemModelQuery, Response<ModelDto>>
        {
            private readonly ILogger<GetItemModelHandler> _logger;
            private readonly IModelRepository _repository;
            public GetItemModelHandler(ILogger<GetItemModelHandler> logger, IModelRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ModelDto>> Handle(GetItemModelQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Name, request.ModuleName);
                return returnValue.ToResponse();
            }
        }
    }
}