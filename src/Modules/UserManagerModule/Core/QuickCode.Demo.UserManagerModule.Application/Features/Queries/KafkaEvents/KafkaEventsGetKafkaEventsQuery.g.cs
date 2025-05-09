using System;
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
    public class KafkaEventsKafkaEventsGetKafkaEventsQuery : IRequest<Response<List<KafkaEventsGetKafkaEventsResponseDto>>>
    {
        public KafkaEventsKafkaEventsGetKafkaEventsQuery()
        {
        }

        public class KafkaEventsKafkaEventsGetKafkaEventsHandler : IRequestHandler<KafkaEventsKafkaEventsGetKafkaEventsQuery, Response<List<KafkaEventsGetKafkaEventsResponseDto>>>
        {
            private readonly ILogger<KafkaEventsKafkaEventsGetKafkaEventsHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsKafkaEventsGetKafkaEventsHandler(ILogger<KafkaEventsKafkaEventsGetKafkaEventsHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<KafkaEventsGetKafkaEventsResponseDto>>> Handle(KafkaEventsKafkaEventsGetKafkaEventsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.KafkaEventsGetKafkaEventsAsync();
                return returnValue.ToResponse();
            }
        }
    }
}