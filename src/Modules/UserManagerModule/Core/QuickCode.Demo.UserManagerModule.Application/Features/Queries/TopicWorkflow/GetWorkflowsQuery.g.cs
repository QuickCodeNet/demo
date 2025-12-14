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
using QuickCode.Demo.UserManagerModule.Application.Dtos.TopicWorkflow;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.TopicWorkflow
{
    public class GetWorkflowsQuery : IRequest<Response<List<GetWorkflowsResponseDto>>>
    {
        public string TopicWorkflowsKafkaEventsTopicName { get; set; }

        public GetWorkflowsQuery(string topicWorkflowsKafkaEventsTopicName)
        {
            this.TopicWorkflowsKafkaEventsTopicName = topicWorkflowsKafkaEventsTopicName;
        }

        public class GetWorkflowsHandler : IRequestHandler<GetWorkflowsQuery, Response<List<GetWorkflowsResponseDto>>>
        {
            private readonly ILogger<GetWorkflowsHandler> _logger;
            private readonly ITopicWorkflowRepository _repository;
            public GetWorkflowsHandler(ILogger<GetWorkflowsHandler> logger, ITopicWorkflowRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetWorkflowsResponseDto>>> Handle(GetWorkflowsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetWorkflowsAsync(request.TopicWorkflowsKafkaEventsTopicName);
                return returnValue.ToResponse();
            }
        }
    }
}