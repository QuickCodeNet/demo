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
    public class GetMessageLogsForMessageTemplatesDetailsQuery : IRequest<Response<GetMessageLogsForMessageTemplatesResponseDto>>
    {
        public string MessageTemplatesName { get; set; }
        public int MessageLogsId { get; set; }

        public GetMessageLogsForMessageTemplatesDetailsQuery(string messageTemplatesName, int messageLogsId)
        {
            this.MessageTemplatesName = messageTemplatesName;
            this.MessageLogsId = messageLogsId;
        }

        public class GetMessageLogsForMessageTemplatesDetailsHandler : IRequestHandler<GetMessageLogsForMessageTemplatesDetailsQuery, Response<GetMessageLogsForMessageTemplatesResponseDto>>
        {
            private readonly ILogger<GetMessageLogsForMessageTemplatesDetailsHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public GetMessageLogsForMessageTemplatesDetailsHandler(ILogger<GetMessageLogsForMessageTemplatesDetailsHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetMessageLogsForMessageTemplatesResponseDto>> Handle(GetMessageLogsForMessageTemplatesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessageLogsForMessageTemplatesDetailsAsync(request.MessageTemplatesName, request.MessageLogsId);
                return returnValue.ToResponse();
            }
        }
    }
}