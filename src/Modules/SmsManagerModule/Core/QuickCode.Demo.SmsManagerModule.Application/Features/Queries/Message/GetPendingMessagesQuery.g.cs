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
    public class GetPendingMessagesQuery : IRequest<Response<List<GetPendingMessagesResponseDto>>>
    {
        public MessageStatus MessagesStatus { get; set; }

        public GetPendingMessagesQuery(MessageStatus messagesStatus)
        {
            this.MessagesStatus = messagesStatus;
        }

        public class GetPendingMessagesHandler : IRequestHandler<GetPendingMessagesQuery, Response<List<GetPendingMessagesResponseDto>>>
        {
            private readonly ILogger<GetPendingMessagesHandler> _logger;
            private readonly IMessageRepository _repository;
            public GetPendingMessagesHandler(ILogger<GetPendingMessagesHandler> logger, IMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetPendingMessagesResponseDto>>> Handle(GetPendingMessagesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetPendingMessagesAsync(request.MessagesStatus);
                return returnValue.ToResponse();
            }
        }
    }
}