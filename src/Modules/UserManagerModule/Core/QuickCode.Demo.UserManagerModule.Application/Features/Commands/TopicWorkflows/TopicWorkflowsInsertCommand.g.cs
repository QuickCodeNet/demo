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
    public class TopicWorkflowsInsertCommand : IRequest<Response<TopicWorkflowsDto>>
    {
        public TopicWorkflowsDto request { get; set; }

        public TopicWorkflowsInsertCommand(TopicWorkflowsDto request)
        {
            this.request = request;
        }

        public class TopicWorkflowsInsertHandler : IRequestHandler<TopicWorkflowsInsertCommand, Response<TopicWorkflowsDto>>
        {
            private readonly ILogger<TopicWorkflowsInsertHandler> _logger;
            private readonly ITopicWorkflowsRepository _repository;
            public TopicWorkflowsInsertHandler(ILogger<TopicWorkflowsInsertHandler> logger, ITopicWorkflowsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<TopicWorkflowsDto>> Handle(TopicWorkflowsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}