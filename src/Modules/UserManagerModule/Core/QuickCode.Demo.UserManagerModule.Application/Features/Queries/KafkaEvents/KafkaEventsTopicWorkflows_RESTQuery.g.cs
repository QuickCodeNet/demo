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
    public class KafkaEventsKafkaEventsTopicWorkflows_RESTQuery : IRequest<Response<List<KafkaEventsTopicWorkflows_RESTResponseDto>>>
    {
        public string KafkaEventsTopicName { get; set; }

        public KafkaEventsKafkaEventsTopicWorkflows_RESTQuery(string kafkaEventsTopicName)
        {
            this.KafkaEventsTopicName = kafkaEventsTopicName;
        }

        public class KafkaEventsKafkaEventsTopicWorkflows_RESTHandler : IRequestHandler<KafkaEventsKafkaEventsTopicWorkflows_RESTQuery, Response<List<KafkaEventsTopicWorkflows_RESTResponseDto>>>
        {
            private readonly ILogger<KafkaEventsKafkaEventsTopicWorkflows_RESTHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsKafkaEventsTopicWorkflows_RESTHandler(ILogger<KafkaEventsKafkaEventsTopicWorkflows_RESTHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<KafkaEventsTopicWorkflows_RESTResponseDto>>> Handle(KafkaEventsKafkaEventsTopicWorkflows_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.KafkaEventsTopicWorkflows_RESTAsync(request.KafkaEventsTopicName);
                return returnValue.ToResponse();
            }
        }
    }
}