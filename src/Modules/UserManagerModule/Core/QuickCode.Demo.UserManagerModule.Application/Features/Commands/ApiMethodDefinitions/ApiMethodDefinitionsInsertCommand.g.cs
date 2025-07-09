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

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class ApiMethodDefinitionsInsertCommand : IRequest<Response<ApiMethodDefinitionsDto>>
    {
        public ApiMethodDefinitionsDto request { get; set; }

        public ApiMethodDefinitionsInsertCommand(ApiMethodDefinitionsDto request)
        {
            this.request = request;
        }

        public class ApiMethodDefinitionsInsertHandler : IRequestHandler<ApiMethodDefinitionsInsertCommand, Response<ApiMethodDefinitionsDto>>
        {
            private readonly ILogger<ApiMethodDefinitionsInsertHandler> _logger;
            private readonly IApiMethodDefinitionsRepository _repository;
            public ApiMethodDefinitionsInsertHandler(ILogger<ApiMethodDefinitionsInsertHandler> logger, IApiMethodDefinitionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApiMethodDefinitionsDto>> Handle(ApiMethodDefinitionsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}