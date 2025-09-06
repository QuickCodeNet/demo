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
    public class TopicWorkflowsDeleteCommand : IRequest<Response<bool>>
    {
        public TopicWorkflowsDto request { get; set; }

        public TopicWorkflowsDeleteCommand(TopicWorkflowsDto request)
        {
            this.request = request;
        }

        public class TopicWorkflowsDeleteHandler : IRequestHandler<TopicWorkflowsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<TopicWorkflowsDeleteHandler> _logger;
            private readonly ITopicWorkflowsRepository _repository;
            public TopicWorkflowsDeleteHandler(ILogger<TopicWorkflowsDeleteHandler> logger, ITopicWorkflowsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(TopicWorkflowsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}