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
    public class UpdateTopicWorkflowCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public TopicWorkflowDto request { get; set; }

        public UpdateTopicWorkflowCommand(int id, TopicWorkflowDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class UpdateTopicWorkflowHandler : IRequestHandler<UpdateTopicWorkflowCommand, Response<bool>>
        {
            private readonly ILogger<UpdateTopicWorkflowHandler> _logger;
            private readonly ITopicWorkflowRepository _repository;
            public UpdateTopicWorkflowHandler(ILogger<UpdateTopicWorkflowHandler> logger, ITopicWorkflowRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateTopicWorkflowCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}