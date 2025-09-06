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
    public class KafkaEventsInsertCommand : IRequest<Response<KafkaEventsDto>>
    {
        public KafkaEventsDto request { get; set; }

        public KafkaEventsInsertCommand(KafkaEventsDto request)
        {
            this.request = request;
        }

        public class KafkaEventsInsertHandler : IRequestHandler<KafkaEventsInsertCommand, Response<KafkaEventsDto>>
        {
            private readonly ILogger<KafkaEventsInsertHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsInsertHandler(ILogger<KafkaEventsInsertHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<KafkaEventsDto>> Handle(KafkaEventsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}