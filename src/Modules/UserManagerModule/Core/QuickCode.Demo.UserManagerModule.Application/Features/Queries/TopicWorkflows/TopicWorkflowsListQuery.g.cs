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
    public class TopicWorkflowsListQuery : IRequest<Response<List<TopicWorkflowsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public TopicWorkflowsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class TopicWorkflowsListHandler : IRequestHandler<TopicWorkflowsListQuery, Response<List<TopicWorkflowsDto>>>
        {
            private readonly ILogger<TopicWorkflowsListHandler> _logger;
            private readonly ITopicWorkflowsRepository _repository;
            public TopicWorkflowsListHandler(ILogger<TopicWorkflowsListHandler> logger, ITopicWorkflowsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<TopicWorkflowsDto>>> Handle(TopicWorkflowsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}