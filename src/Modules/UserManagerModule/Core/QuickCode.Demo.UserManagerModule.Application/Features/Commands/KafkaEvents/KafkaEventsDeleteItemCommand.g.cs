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
    public class KafkaEventsDeleteItemCommand : IRequest<Response<bool>>
    {
        public string TopicName { get; set; }

        public KafkaEventsDeleteItemCommand(string topicName)
        {
            this.TopicName = topicName;
        }

        public class KafkaEventsDeleteItemHandler : IRequestHandler<KafkaEventsDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<KafkaEventsDeleteItemHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsDeleteItemHandler(ILogger<KafkaEventsDeleteItemHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(KafkaEventsDeleteItemCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.TopicName);
                if (deleteItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}