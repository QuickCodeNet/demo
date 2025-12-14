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
    public class GetPendingQueueQuery : IRequest<Response<List<GetPendingQueueResponseDto>>>
    {
        public MessageStatus MessageQueuesStatus { get; set; }

        public GetPendingQueueQuery(MessageStatus messageQueuesStatus)
        {
            this.MessageQueuesStatus = messageQueuesStatus;
        }

        public class GetPendingQueueHandler : IRequestHandler<GetPendingQueueQuery, Response<List<GetPendingQueueResponseDto>>>
        {
            private readonly ILogger<GetPendingQueueHandler> _logger;
            private readonly IMessageQueueRepository _repository;
            public GetPendingQueueHandler(ILogger<GetPendingQueueHandler> logger, IMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetPendingQueueResponseDto>>> Handle(GetPendingQueueQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetPendingQueueAsync(request.MessageQueuesStatus);
                return returnValue.ToResponse();
            }
        }
    }
}