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
    public class GetWORKFLOWS2Query : IRequest<Response<List<GetWORKFLOWS2ResponseDto>>>
    {
        public string TopicWorkflowsKafkaEventsTopicName { get; set; }

        public GetWORKFLOWS2Query(string topicWorkflowsKafkaEventsTopicName)
        {
            this.TopicWorkflowsKafkaEventsTopicName = topicWorkflowsKafkaEventsTopicName;
        }

        public class GetWORKFLOWS2Handler : IRequestHandler<GetWORKFLOWS2Query, Response<List<GetWORKFLOWS2ResponseDto>>>
        {
            private readonly ILogger<GetWORKFLOWS2Handler> _logger;
            private readonly ITopicWorkflowRepository _repository;
            public GetWORKFLOWS2Handler(ILogger<GetWORKFLOWS2Handler> logger, ITopicWorkflowRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetWORKFLOWS2ResponseDto>>> Handle(GetWORKFLOWS2Query request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetWORKFLOWS2Async(request.TopicWorkflowsKafkaEventsTopicName);
                return returnValue.ToResponse();
            }
        }
    }
}