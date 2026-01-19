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
using QuickCode.Demo.UserManagerModule.Application.Dtos.KafkaEvent;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.KafkaEvent
{
    public class UpdateKafkaEventCommand : IRequest<Response<bool>>
    {
        public string TopicName { get; set; }
        public KafkaEventDto request { get; set; }

        public UpdateKafkaEventCommand(string topicName, KafkaEventDto request)
        {
            this.request = request;
            this.TopicName = topicName;
        }

        public class UpdateKafkaEventHandler : IRequestHandler<UpdateKafkaEventCommand, Response<bool>>
        {
            private readonly ILogger<UpdateKafkaEventHandler> _logger;
            private readonly IKafkaEventRepository _repository;
            public UpdateKafkaEventHandler(ILogger<UpdateKafkaEventHandler> logger, IKafkaEventRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateKafkaEventCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.TopicName);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}