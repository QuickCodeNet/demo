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
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class KafkaEventsGetActiveKafkaEventsQuery : IRequest<Response<List<KafkaEventsGetActiveKafkaEventsResponseDto>>>
    {
        public KafkaEventsGetActiveKafkaEventsQuery()
        {
        }

        public class KafkaEventsGetActiveKafkaEventsHandler : IRequestHandler<KafkaEventsGetActiveKafkaEventsQuery, Response<List<KafkaEventsGetActiveKafkaEventsResponseDto>>>
        {
            private readonly ILogger<KafkaEventsGetActiveKafkaEventsHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsGetActiveKafkaEventsHandler(ILogger<KafkaEventsGetActiveKafkaEventsHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<KafkaEventsGetActiveKafkaEventsResponseDto>>> Handle(KafkaEventsGetActiveKafkaEventsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.KafkaEventsGetActiveKafkaEventsAsync();
                return returnValue.ToResponse();
            }
        }
    }
}