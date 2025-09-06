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
    public class TopicWorkflowsGetItemQuery : IRequest<Response<TopicWorkflowsDto>>
    {
        public int Id { get; set; }

        public TopicWorkflowsGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class TopicWorkflowsGetItemHandler : IRequestHandler<TopicWorkflowsGetItemQuery, Response<TopicWorkflowsDto>>
        {
            private readonly ILogger<TopicWorkflowsGetItemHandler> _logger;
            private readonly ITopicWorkflowsRepository _repository;
            public TopicWorkflowsGetItemHandler(ILogger<TopicWorkflowsGetItemHandler> logger, ITopicWorkflowsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<TopicWorkflowsDto>> Handle(TopicWorkflowsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}