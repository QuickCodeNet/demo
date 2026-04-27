using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.IdentityModule.Domain.Entities;
using QuickCode.Demo.IdentityModule.Application.Interfaces.Repositories;
using QuickCode.Demo.IdentityModule.Application.Dtos.KafkaEvent;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.KafkaEvent
{
    public class DeleteKafkaEventCommand : IRequest<Response<bool>>
    {
        public KafkaEventDto request { get; set; }

        public DeleteKafkaEventCommand(KafkaEventDto request)
        {
            this.request = request;
        }

        public class DeleteKafkaEventHandler : IRequestHandler<DeleteKafkaEventCommand, Response<bool>>
        {
            private readonly ILogger<DeleteKafkaEventHandler> _logger;
            private readonly IKafkaEventRepository _repository;
            public DeleteKafkaEventHandler(ILogger<DeleteKafkaEventHandler> logger, IKafkaEventRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteKafkaEventCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}