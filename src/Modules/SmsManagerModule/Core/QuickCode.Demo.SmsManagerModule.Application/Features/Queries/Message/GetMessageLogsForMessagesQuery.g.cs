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
    public class GetMessageLogsForMessagesQuery : IRequest<Response<List<GetMessageLogsForMessagesResponseDto>>>
    {
        public int MessagesId { get; set; }

        public GetMessageLogsForMessagesQuery(int messagesId)
        {
            this.MessagesId = messagesId;
        }

        public class GetMessageLogsForMessagesHandler : IRequestHandler<GetMessageLogsForMessagesQuery, Response<List<GetMessageLogsForMessagesResponseDto>>>
        {
            private readonly ILogger<GetMessageLogsForMessagesHandler> _logger;
            private readonly IMessageRepository _repository;
            public GetMessageLogsForMessagesHandler(ILogger<GetMessageLogsForMessagesHandler> logger, IMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetMessageLogsForMessagesResponseDto>>> Handle(GetMessageLogsForMessagesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessageLogsForMessagesAsync(request.MessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}