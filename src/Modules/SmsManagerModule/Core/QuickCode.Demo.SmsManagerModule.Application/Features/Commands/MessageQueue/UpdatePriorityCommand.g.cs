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
    public class UpdatePriorityCommand : IRequest<Response<int>>
    {
        public int MessageQueuesId { get; set; }
        public UpdatePriorityRequestDto UpdateRequest { get; set; }

        public UpdatePriorityCommand(int messageQueuesId, UpdatePriorityRequestDto updateRequest)
        {
            this.MessageQueuesId = messageQueuesId;
            this.UpdateRequest = updateRequest;
        }

        public class UpdatePriorityHandler : IRequestHandler<UpdatePriorityCommand, Response<int>>
        {
            private readonly ILogger<UpdatePriorityHandler> _logger;
            private readonly IMessageQueueRepository _repository;
            public UpdatePriorityHandler(ILogger<UpdatePriorityHandler> logger, IMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(UpdatePriorityCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.UpdatePriorityAsync(request.MessageQueuesId, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}