using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class ApiMethodDefinitionsDeleteCommand : IRequest<Response<bool>>
    {
        public ApiMethodDefinitionsDto request { get; set; }

        public ApiMethodDefinitionsDeleteCommand(ApiMethodDefinitionsDto request)
        {
            this.request = request;
        }

        public class ApiMethodDefinitionsDeleteHandler : IRequestHandler<ApiMethodDefinitionsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<ApiMethodDefinitionsDeleteHandler> _logger;
            private readonly IApiMethodDefinitionsRepository _repository;
            public ApiMethodDefinitionsDeleteHandler(ILogger<ApiMethodDefinitionsDeleteHandler> logger, IApiMethodDefinitionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ApiMethodDefinitionsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}