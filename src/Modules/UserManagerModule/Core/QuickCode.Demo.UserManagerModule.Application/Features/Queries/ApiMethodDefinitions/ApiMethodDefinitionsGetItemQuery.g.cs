using System.Linq;
using MediatR;
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
    public class ApiMethodDefinitionsGetItemQuery : IRequest<Response<ApiMethodDefinitionsDto>>
    {
        public int Id { get; set; }

        public ApiMethodDefinitionsGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class ApiMethodDefinitionsGetItemHandler : IRequestHandler<ApiMethodDefinitionsGetItemQuery, Response<ApiMethodDefinitionsDto>>
        {
            private readonly ILogger<ApiMethodDefinitionsGetItemHandler> _logger;
            private readonly IApiMethodDefinitionsRepository _repository;
            public ApiMethodDefinitionsGetItemHandler(ILogger<ApiMethodDefinitionsGetItemHandler> logger, IApiMethodDefinitionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApiMethodDefinitionsDto>> Handle(ApiMethodDefinitionsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}