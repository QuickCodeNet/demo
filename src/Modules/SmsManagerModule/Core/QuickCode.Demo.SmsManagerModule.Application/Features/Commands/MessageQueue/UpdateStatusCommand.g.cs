using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageQueue;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.MessageQueue
{
    public class UpdateStatusCommand : IRequest<Response<int>>
    {
        public int MessageQueuesId { get; set; }
        public UpdateStatusRequestDto UpdateRequest { get; set; }

        public UpdateStatusCommand(int messageQueuesId, UpdateStatusRequestDto updateRequest)
        {
            this.MessageQueuesId = messageQueuesId;
            this.UpdateRequest = updateRequest;
        }

        public class UpdateStatusHandler : IRequestHandler<UpdateStatusCommand, Response<int>>
        {
            private readonly ILogger<UpdateStatusHandler> _logger;
            private readonly IMessageQueueRepository _repository;
            public UpdateStatusHandler(ILogger<UpdateStatusHandler> logger, IMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.UpdateStatusAsync(request.MessageQueuesId, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}