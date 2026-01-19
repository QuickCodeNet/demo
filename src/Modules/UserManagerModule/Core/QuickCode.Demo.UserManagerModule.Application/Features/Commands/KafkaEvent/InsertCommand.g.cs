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
    public class InsertKafkaEventCommand : IRequest<Response<KafkaEventDto>>
    {
        public KafkaEventDto request { get; set; }

        public InsertKafkaEventCommand(KafkaEventDto request)
        {
            this.request = request;
        }

        public class InsertKafkaEventHandler : IRequestHandler<InsertKafkaEventCommand, Response<KafkaEventDto>>
        {
            private readonly ILogger<InsertKafkaEventHandler> _logger;
            private readonly IKafkaEventRepository _repository;
            public InsertKafkaEventHandler(ILogger<InsertKafkaEventHandler> logger, IKafkaEventRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<KafkaEventDto>> Handle(InsertKafkaEventCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}