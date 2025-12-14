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
    public class GetMessageQueuesForMessagesDetailsQuery : IRequest<Response<GetMessageQueuesForMessagesResponseDto>>
    {
        public int MessagesId { get; set; }
        public int MessageQueuesId { get; set; }

        public GetMessageQueuesForMessagesDetailsQuery(int messagesId, int messageQueuesId)
        {
            this.MessagesId = messagesId;
            this.MessageQueuesId = messageQueuesId;
        }

        public class GetMessageQueuesForMessagesDetailsHandler : IRequestHandler<GetMessageQueuesForMessagesDetailsQuery, Response<GetMessageQueuesForMessagesResponseDto>>
        {
            private readonly ILogger<GetMessageQueuesForMessagesDetailsHandler> _logger;
            private readonly IMessageRepository _repository;
            public GetMessageQueuesForMessagesDetailsHandler(ILogger<GetMessageQueuesForMessagesDetailsHandler> logger, IMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetMessageQueuesForMessagesResponseDto>> Handle(GetMessageQueuesForMessagesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessageQueuesForMessagesDetailsAsync(request.MessagesId, request.MessageQueuesId);
                return returnValue.ToResponse();
            }
        }
    }
}