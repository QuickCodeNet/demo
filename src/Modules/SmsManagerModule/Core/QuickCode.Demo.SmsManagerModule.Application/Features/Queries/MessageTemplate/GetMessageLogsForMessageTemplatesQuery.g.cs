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
    public class GetMessageLogsForMessageTemplatesQuery : IRequest<Response<List<GetMessageLogsForMessageTemplatesResponseDto>>>
    {
        public string MessageTemplatesName { get; set; }

        public GetMessageLogsForMessageTemplatesQuery(string messageTemplatesName)
        {
            this.MessageTemplatesName = messageTemplatesName;
        }

        public class GetMessageLogsForMessageTemplatesHandler : IRequestHandler<GetMessageLogsForMessageTemplatesQuery, Response<List<GetMessageLogsForMessageTemplatesResponseDto>>>
        {
            private readonly ILogger<GetMessageLogsForMessageTemplatesHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public GetMessageLogsForMessageTemplatesHandler(ILogger<GetMessageLogsForMessageTemplatesHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetMessageLogsForMessageTemplatesResponseDto>>> Handle(GetMessageLogsForMessageTemplatesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessageLogsForMessageTemplatesAsync(request.MessageTemplatesName);
                return returnValue.ToResponse();
            }
        }
    }
}