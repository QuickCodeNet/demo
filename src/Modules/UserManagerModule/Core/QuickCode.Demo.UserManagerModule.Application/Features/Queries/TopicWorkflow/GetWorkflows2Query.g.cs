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
    public class GetWorkflows2Query : IRequest<Response<List<GetWorkflows2ResponseDto>>>
    {
        public string TopicWorkflowsKafkaEventsTopicName { get; set; }

        public GetWorkflows2Query(string topicWorkflowsKafkaEventsTopicName)
        {
            this.TopicWorkflowsKafkaEventsTopicName = topicWorkflowsKafkaEventsTopicName;
        }

        public class GetWorkflows2Handler : IRequestHandler<GetWorkflows2Query, Response<List<GetWorkflows2ResponseDto>>>
        {
            private readonly ILogger<GetWorkflows2Handler> _logger;
            private readonly ITopicWorkflowRepository _repository;
            public GetWorkflows2Handler(ILogger<GetWorkflows2Handler> logger, ITopicWorkflowRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetWorkflows2ResponseDto>>> Handle(GetWorkflows2Query request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetWorkflows2Async(request.TopicWorkflowsKafkaEventsTopicName);
                return returnValue.ToResponse();
            }
        }
    }
}