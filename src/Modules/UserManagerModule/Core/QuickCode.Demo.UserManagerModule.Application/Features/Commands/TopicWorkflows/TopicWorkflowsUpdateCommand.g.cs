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
    public class TopicWorkflowsUpdateCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public TopicWorkflowsDto request { get; set; }

        public TopicWorkflowsUpdateCommand(int id, TopicWorkflowsDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class TopicWorkflowsUpdateHandler : IRequestHandler<TopicWorkflowsUpdateCommand, Response<bool>>
        {
            private readonly ILogger<TopicWorkflowsUpdateHandler> _logger;
            private readonly ITopicWorkflowsRepository _repository;
            public TopicWorkflowsUpdateHandler(ILogger<TopicWorkflowsUpdateHandler> logger, ITopicWorkflowsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(TopicWorkflowsUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}