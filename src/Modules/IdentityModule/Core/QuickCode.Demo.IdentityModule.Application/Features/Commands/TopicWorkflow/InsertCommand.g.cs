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
using QuickCode.Demo.IdentityModule.Application.Dtos.TopicWorkflow;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.TopicWorkflow
{
    public class InsertTopicWorkflowCommand : IRequest<Response<TopicWorkflowDto>>
    {
        public TopicWorkflowDto request { get; set; }

        public InsertTopicWorkflowCommand(TopicWorkflowDto request)
        {
            this.request = request;
        }

        public class InsertTopicWorkflowHandler : IRequestHandler<InsertTopicWorkflowCommand, Response<TopicWorkflowDto>>
        {
            private readonly ILogger<InsertTopicWorkflowHandler> _logger;
            private readonly ITopicWorkflowRepository _repository;
            public InsertTopicWorkflowHandler(ILogger<InsertTopicWorkflowHandler> logger, ITopicWorkflowRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<TopicWorkflowDto>> Handle(InsertTopicWorkflowCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}