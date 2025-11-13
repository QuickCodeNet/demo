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
    public class DeleteItemTopicWorkflowCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public DeleteItemTopicWorkflowCommand(int id)
        {
            this.Id = id;
        }

        public class DeleteItemTopicWorkflowHandler : IRequestHandler<DeleteItemTopicWorkflowCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemTopicWorkflowHandler> _logger;
            private readonly ITopicWorkflowRepository _repository;
            public DeleteItemTopicWorkflowHandler(ILogger<DeleteItemTopicWorkflowHandler> logger, ITopicWorkflowRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemTopicWorkflowCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Id);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}