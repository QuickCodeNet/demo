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
    public class GetTopicWorkflowsKafkaEventsQuery : IRequest<Response<List<GetTopicWorkflowsKafkaEventsResponseDto>>>
    {
        public string KafkaEventsTopicName { get; set; }
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }

        public GetTopicWorkflowsKafkaEventsQuery(string kafkaEventsTopicName, int? pageNumber, int? pageSize)
        {
            this.KafkaEventsTopicName = kafkaEventsTopicName;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }

        public class GetTopicWorkflowsKafkaEventsHandler : IRequestHandler<GetTopicWorkflowsKafkaEventsQuery, Response<List<GetTopicWorkflowsKafkaEventsResponseDto>>>
        {
            private readonly ILogger<GetTopicWorkflowsKafkaEventsHandler> _logger;
            private readonly IKafkaEventRepository _repository;
            public GetTopicWorkflowsKafkaEventsHandler(ILogger<GetTopicWorkflowsKafkaEventsHandler> logger, IKafkaEventRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetTopicWorkflowsKafkaEventsResponseDto>>> Handle(GetTopicWorkflowsKafkaEventsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetTopicWorkflowsKafkaEventsAsync(request.KafkaEventsTopicName, request.pageNumber, request.pageSize);
                return returnValue.ToResponse();
            }
        }
    }
}