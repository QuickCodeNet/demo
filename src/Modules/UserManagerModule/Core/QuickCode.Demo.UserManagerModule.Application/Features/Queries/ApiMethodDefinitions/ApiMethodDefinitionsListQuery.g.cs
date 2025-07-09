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
    public class ApiMethodDefinitionsListQuery : IRequest<Response<List<ApiMethodDefinitionsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ApiMethodDefinitionsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ApiMethodDefinitionsListHandler : IRequestHandler<ApiMethodDefinitionsListQuery, Response<List<ApiMethodDefinitionsDto>>>
        {
            private readonly ILogger<ApiMethodDefinitionsListHandler> _logger;
            private readonly IApiMethodDefinitionsRepository _repository;
            public ApiMethodDefinitionsListHandler(ILogger<ApiMethodDefinitionsListHandler> logger, IApiMethodDefinitionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApiMethodDefinitionsDto>>> Handle(ApiMethodDefinitionsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}