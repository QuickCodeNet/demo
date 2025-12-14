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
    public class UpdatePriorityCommand : IRequest<Response<int>>
    {
        public int OtpMessageQueuesId { get; set; }
        public UpdatePriorityRequestDto UpdateRequest { get; set; }

        public UpdatePriorityCommand(int otpMessageQueuesId, UpdatePriorityRequestDto updateRequest)
        {
            this.OtpMessageQueuesId = otpMessageQueuesId;
            this.UpdateRequest = updateRequest;
        }

        public class UpdatePriorityHandler : IRequestHandler<UpdatePriorityCommand, Response<int>>
        {
            private readonly ILogger<UpdatePriorityHandler> _logger;
            private readonly IOtpMessageQueueRepository _repository;
            public UpdatePriorityHandler(ILogger<UpdatePriorityHandler> logger, IOtpMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(UpdatePriorityCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.UpdatePriorityAsync(request.OtpMessageQueuesId, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}