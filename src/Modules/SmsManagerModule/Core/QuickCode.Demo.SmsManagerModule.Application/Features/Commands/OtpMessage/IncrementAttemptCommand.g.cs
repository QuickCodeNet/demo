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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessage;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessage
{
    public class IncrementAttemptCommand : IRequest<Response<int>>
    {
        public int OtpMessagesId { get; set; }
        public IncrementAttemptRequestDto UpdateRequest { get; set; }

        public IncrementAttemptCommand(int otpMessagesId, IncrementAttemptRequestDto updateRequest)
        {
            this.OtpMessagesId = otpMessagesId;
            this.UpdateRequest = updateRequest;
        }

        public class IncrementAttemptHandler : IRequestHandler<IncrementAttemptCommand, Response<int>>
        {
            private readonly ILogger<IncrementAttemptHandler> _logger;
            private readonly IOtpMessageRepository _repository;
            public IncrementAttemptHandler(ILogger<IncrementAttemptHandler> logger, IOtpMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(IncrementAttemptCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.IncrementAttemptAsync(request.OtpMessagesId, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}