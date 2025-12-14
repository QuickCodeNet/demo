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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessageQueue;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessageQueue
{
    public class GetPendingQueueQuery : IRequest<Response<List<GetPendingQueueResponseDto>>>
    {
        public MessageStatus OtpMessageQueuesStatus { get; set; }

        public GetPendingQueueQuery(MessageStatus otpMessageQueuesStatus)
        {
            this.OtpMessageQueuesStatus = otpMessageQueuesStatus;
        }

        public class GetPendingQueueHandler : IRequestHandler<GetPendingQueueQuery, Response<List<GetPendingQueueResponseDto>>>
        {
            private readonly ILogger<GetPendingQueueHandler> _logger;
            private readonly IOtpMessageQueueRepository _repository;
            public GetPendingQueueHandler(ILogger<GetPendingQueueHandler> logger, IOtpMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetPendingQueueResponseDto>>> Handle(GetPendingQueueQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetPendingQueueAsync(request.OtpMessageQueuesStatus);
                return returnValue.ToResponse();
            }
        }
    }
}