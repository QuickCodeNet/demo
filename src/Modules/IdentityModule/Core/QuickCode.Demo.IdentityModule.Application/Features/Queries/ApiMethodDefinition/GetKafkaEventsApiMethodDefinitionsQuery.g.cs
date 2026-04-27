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
using QuickCode.Demo.IdentityModule.Application.Dtos.ApiMethodDefinition;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.ApiMethodDefinition
{
    public class GetKafkaEventsApiMethodDefinitionsQuery : IRequest<Response<List<GetKafkaEventsApiMethodDefinitionsResponseDto>>>
    {
        public string ApiMethodDefinitionsKey { get; set; }
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }

        public GetKafkaEventsApiMethodDefinitionsQuery(string apiMethodDefinitionsKey, int? pageNumber, int? pageSize)
        {
            this.ApiMethodDefinitionsKey = apiMethodDefinitionsKey;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }

        public class GetKafkaEventsApiMethodDefinitionsHandler : IRequestHandler<GetKafkaEventsApiMethodDefinitionsQuery, Response<List<GetKafkaEventsApiMethodDefinitionsResponseDto>>>
        {
            private readonly ILogger<GetKafkaEventsApiMethodDefinitionsHandler> _logger;
            private readonly IApiMethodDefinitionRepository _repository;
            public GetKafkaEventsApiMethodDefinitionsHandler(ILogger<GetKafkaEventsApiMethodDefinitionsHandler> logger, IApiMethodDefinitionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetKafkaEventsApiMethodDefinitionsResponseDto>>> Handle(GetKafkaEventsApiMethodDefinitionsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetKafkaEventsApiMethodDefinitionsAsync(request.ApiMethodDefinitionsKey, request.pageNumber, request.pageSize);
                return returnValue.ToResponse();
            }
        }
    }
}