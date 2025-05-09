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
    public class TopicWorkflowsTotalItemCountQuery : IRequest<Response<int>>
    {
        public TopicWorkflowsTotalItemCountQuery()
        {
        }

        public class TopicWorkflowsTotalItemCountHandler : IRequestHandler<TopicWorkflowsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<TopicWorkflowsTotalItemCountHandler> _logger;
            private readonly ITopicWorkflowsRepository _repository;
            public TopicWorkflowsTotalItemCountHandler(ILogger<TopicWorkflowsTotalItemCountHandler> logger, ITopicWorkflowsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TopicWorkflowsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}