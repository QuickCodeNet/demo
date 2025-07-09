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
    public class TopicWorkflowsGetTopicWorkflowsQuery : IRequest<Response<List<TopicWorkflowsGetTopicWorkflowsResponseDto>>>
    {
        public string KafkaEventsTopicName { get; set; }
        public string ApiMethodDefinitionsHttpMethod { get; set; }

        public TopicWorkflowsGetTopicWorkflowsQuery(string kafkaEventsTopicName, string apiMethodDefinitionsHttpMethod)
        {
            this.KafkaEventsTopicName = kafkaEventsTopicName;
            this.ApiMethodDefinitionsHttpMethod = apiMethodDefinitionsHttpMethod;
        }

        public class TopicWorkflowsGetTopicWorkflowsHandler : IRequestHandler<TopicWorkflowsGetTopicWorkflowsQuery, Response<List<TopicWorkflowsGetTopicWorkflowsResponseDto>>>
        {
            private readonly ILogger<TopicWorkflowsGetTopicWorkflowsHandler> _logger;
            private readonly ITopicWorkflowsRepository _repository;
            public TopicWorkflowsGetTopicWorkflowsHandler(ILogger<TopicWorkflowsGetTopicWorkflowsHandler> logger, ITopicWorkflowsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<TopicWorkflowsGetTopicWorkflowsResponseDto>>> Handle(TopicWorkflowsGetTopicWorkflowsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.TopicWorkflowsGetTopicWorkflowsAsync(request.KafkaEventsTopicName, request.ApiMethodDefinitionsHttpMethod);
                return returnValue.ToResponse();
            }
        }
    }
}