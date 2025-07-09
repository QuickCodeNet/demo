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
    public class KafkaEventsGetKafkaEventsQuery : IRequest<Response<List<KafkaEventsGetKafkaEventsResponseDto>>>
    {
        public KafkaEventsGetKafkaEventsQuery()
        {
        }

        public class KafkaEventsGetKafkaEventsHandler : IRequestHandler<KafkaEventsGetKafkaEventsQuery, Response<List<KafkaEventsGetKafkaEventsResponseDto>>>
        {
            private readonly ILogger<KafkaEventsGetKafkaEventsHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsGetKafkaEventsHandler(ILogger<KafkaEventsGetKafkaEventsHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<KafkaEventsGetKafkaEventsResponseDto>>> Handle(KafkaEventsGetKafkaEventsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.KafkaEventsGetKafkaEventsAsync();
                return returnValue.ToResponse();
            }
        }
    }
}