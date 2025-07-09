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
    public class KafkaEventsUpdateCommand : IRequest<Response<bool>>
    {
        public string TopicName { get; set; }
        public KafkaEventsDto request { get; set; }

        public KafkaEventsUpdateCommand(string topicName, KafkaEventsDto request)
        {
            this.request = request;
            this.TopicName = topicName;
        }

        public class KafkaEventsUpdateHandler : IRequestHandler<KafkaEventsUpdateCommand, Response<bool>>
        {
            private readonly ILogger<KafkaEventsUpdateHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsUpdateHandler(ILogger<KafkaEventsUpdateHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(KafkaEventsUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.TopicName);
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