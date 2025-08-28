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
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class TopicWorkflowsGetWORKFLOWS2Query : IRequest<Response<List<TopicWorkflowsGetWORKFLOWS2ResponseDto>>>
    {
        public string TopicWorkflowsKafkaEventsTopicName { get; set; }

        public TopicWorkflowsGetWORKFLOWS2Query(string topicWorkflowsKafkaEventsTopicName)
        {
            this.TopicWorkflowsKafkaEventsTopicName = topicWorkflowsKafkaEventsTopicName;
        }

        public class TopicWorkflowsGetWORKFLOWS2Handler : IRequestHandler<TopicWorkflowsGetWORKFLOWS2Query, Response<List<TopicWorkflowsGetWORKFLOWS2ResponseDto>>>
        {
            private readonly ILogger<TopicWorkflowsGetWORKFLOWS2Handler> _logger;
            private readonly ITopicWorkflowsRepository _repository;
            public TopicWorkflowsGetWORKFLOWS2Handler(ILogger<TopicWorkflowsGetWORKFLOWS2Handler> logger, ITopicWorkflowsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<TopicWorkflowsGetWORKFLOWS2ResponseDto>>> Handle(TopicWorkflowsGetWORKFLOWS2Query request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.TopicWorkflowsGetWORKFLOWS2Async(request.TopicWorkflowsKafkaEventsTopicName);
                return returnValue.ToResponse();
            }
        }
    }
}