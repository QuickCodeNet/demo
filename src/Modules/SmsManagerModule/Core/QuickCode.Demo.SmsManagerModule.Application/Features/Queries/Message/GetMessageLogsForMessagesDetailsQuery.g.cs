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
    public class GetMessageLogsForMessagesDetailsQuery : IRequest<Response<GetMessageLogsForMessagesResponseDto>>
    {
        public int MessagesId { get; set; }
        public int MessageLogsId { get; set; }

        public GetMessageLogsForMessagesDetailsQuery(int messagesId, int messageLogsId)
        {
            this.MessagesId = messagesId;
            this.MessageLogsId = messageLogsId;
        }

        public class GetMessageLogsForMessagesDetailsHandler : IRequestHandler<GetMessageLogsForMessagesDetailsQuery, Response<GetMessageLogsForMessagesResponseDto>>
        {
            private readonly ILogger<GetMessageLogsForMessagesDetailsHandler> _logger;
            private readonly IMessageRepository _repository;
            public GetMessageLogsForMessagesDetailsHandler(ILogger<GetMessageLogsForMessagesDetailsHandler> logger, IMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetMessageLogsForMessagesResponseDto>> Handle(GetMessageLogsForMessagesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessageLogsForMessagesDetailsAsync(request.MessagesId, request.MessageLogsId);
                return returnValue.ToResponse();
            }
        }
    }
}