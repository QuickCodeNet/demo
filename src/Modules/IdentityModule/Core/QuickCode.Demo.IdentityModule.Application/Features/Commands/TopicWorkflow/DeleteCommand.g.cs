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
    public class DeleteTopicWorkflowCommand : IRequest<Response<bool>>
    {
        public TopicWorkflowDto request { get; set; }

        public DeleteTopicWorkflowCommand(TopicWorkflowDto request)
        {
            this.request = request;
        }

        public class DeleteTopicWorkflowHandler : IRequestHandler<DeleteTopicWorkflowCommand, Response<bool>>
        {
            private readonly ILogger<DeleteTopicWorkflowHandler> _logger;
            private readonly ITopicWorkflowRepository _repository;
            public DeleteTopicWorkflowHandler(ILogger<DeleteTopicWorkflowHandler> logger, ITopicWorkflowRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteTopicWorkflowCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}