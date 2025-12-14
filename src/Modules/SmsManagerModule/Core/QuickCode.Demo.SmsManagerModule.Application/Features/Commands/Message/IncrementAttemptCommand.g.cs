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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.Message;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.Message
{
    public class IncrementAttemptCommand : IRequest<Response<int>>
    {
        public int MessagesId { get; set; }
        public IncrementAttemptRequestDto UpdateRequest { get; set; }

        public IncrementAttemptCommand(int messagesId, IncrementAttemptRequestDto updateRequest)
        {
            this.MessagesId = messagesId;
            this.UpdateRequest = updateRequest;
        }

        public class IncrementAttemptHandler : IRequestHandler<IncrementAttemptCommand, Response<int>>
        {
            private readonly ILogger<IncrementAttemptHandler> _logger;
            private readonly IMessageRepository _repository;
            public IncrementAttemptHandler(ILogger<IncrementAttemptHandler> logger, IMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(IncrementAttemptCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.IncrementAttemptAsync(request.MessagesId, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}