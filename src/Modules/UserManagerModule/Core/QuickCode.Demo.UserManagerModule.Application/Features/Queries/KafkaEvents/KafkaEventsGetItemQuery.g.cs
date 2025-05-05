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
    public class KafkaEventsGetItemQuery : IRequest<Response<KafkaEventsDto>>
    {
        public string TopicName { get; set; }

        public KafkaEventsGetItemQuery(string topicName)
        {
            this.TopicName = topicName;
        }

        public class KafkaEventsGetItemHandler : IRequestHandler<KafkaEventsGetItemQuery, Response<KafkaEventsDto>>
        {
            private readonly ILogger<KafkaEventsGetItemHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsGetItemHandler(ILogger<KafkaEventsGetItemHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<KafkaEventsDto>> Handle(KafkaEventsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.TopicName);
                return returnValue.ToResponse();
            }
        }
    }
}