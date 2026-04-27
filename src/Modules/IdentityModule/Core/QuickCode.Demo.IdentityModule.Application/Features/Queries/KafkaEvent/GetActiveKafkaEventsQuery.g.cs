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
using QuickCode.Demo.IdentityModule.Application.Dtos.KafkaEvent;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.KafkaEvent
{
    public class GetActiveKafkaEventsQuery : IRequest<Response<List<GetActiveKafkaEventsResponseDto>>>
    {
        public GetActiveKafkaEventsQuery()
        {
        }

        public class GetActiveKafkaEventsHandler : IRequestHandler<GetActiveKafkaEventsQuery, Response<List<GetActiveKafkaEventsResponseDto>>>
        {
            private readonly ILogger<GetActiveKafkaEventsHandler> _logger;
            private readonly IKafkaEventRepository _repository;
            public GetActiveKafkaEventsHandler(ILogger<GetActiveKafkaEventsHandler> logger, IKafkaEventRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetActiveKafkaEventsResponseDto>>> Handle(GetActiveKafkaEventsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetActiveKafkaEventsAsync();
                return returnValue.ToResponse();
            }
        }
    }
}