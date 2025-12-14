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
    public class GetMessageQueuesForMessagesQuery : IRequest<Response<List<GetMessageQueuesForMessagesResponseDto>>>
    {
        public int MessagesId { get; set; }

        public GetMessageQueuesForMessagesQuery(int messagesId)
        {
            this.MessagesId = messagesId;
        }

        public class GetMessageQueuesForMessagesHandler : IRequestHandler<GetMessageQueuesForMessagesQuery, Response<List<GetMessageQueuesForMessagesResponseDto>>>
        {
            private readonly ILogger<GetMessageQueuesForMessagesHandler> _logger;
            private readonly IMessageRepository _repository;
            public GetMessageQueuesForMessagesHandler(ILogger<GetMessageQueuesForMessagesHandler> logger, IMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetMessageQueuesForMessagesResponseDto>>> Handle(GetMessageQueuesForMessagesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessageQueuesForMessagesAsync(request.MessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}