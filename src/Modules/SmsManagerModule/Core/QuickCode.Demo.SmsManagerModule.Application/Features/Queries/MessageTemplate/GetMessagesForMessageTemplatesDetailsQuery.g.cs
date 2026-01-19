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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageTemplate;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.MessageTemplate
{
    public class GetMessagesForMessageTemplatesDetailsQuery : IRequest<Response<GetMessagesForMessageTemplatesResponseDto>>
    {
        public string MessageTemplatesName { get; set; }
        public int MessagesId { get; set; }

        public GetMessagesForMessageTemplatesDetailsQuery(string messageTemplatesName, int messagesId)
        {
            this.MessageTemplatesName = messageTemplatesName;
            this.MessagesId = messagesId;
        }

        public class GetMessagesForMessageTemplatesDetailsHandler : IRequestHandler<GetMessagesForMessageTemplatesDetailsQuery, Response<GetMessagesForMessageTemplatesResponseDto>>
        {
            private readonly ILogger<GetMessagesForMessageTemplatesDetailsHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public GetMessagesForMessageTemplatesDetailsHandler(ILogger<GetMessagesForMessageTemplatesDetailsHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetMessagesForMessageTemplatesResponseDto>> Handle(GetMessagesForMessageTemplatesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessagesForMessageTemplatesDetailsAsync(request.MessageTemplatesName, request.MessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}