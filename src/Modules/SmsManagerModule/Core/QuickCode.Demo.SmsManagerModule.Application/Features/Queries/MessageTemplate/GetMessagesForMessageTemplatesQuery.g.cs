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
    public class GetMessagesForMessageTemplatesQuery : IRequest<Response<List<GetMessagesForMessageTemplatesResponseDto>>>
    {
        public string MessageTemplatesName { get; set; }

        public GetMessagesForMessageTemplatesQuery(string messageTemplatesName)
        {
            this.MessageTemplatesName = messageTemplatesName;
        }

        public class GetMessagesForMessageTemplatesHandler : IRequestHandler<GetMessagesForMessageTemplatesQuery, Response<List<GetMessagesForMessageTemplatesResponseDto>>>
        {
            private readonly ILogger<GetMessagesForMessageTemplatesHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public GetMessagesForMessageTemplatesHandler(ILogger<GetMessagesForMessageTemplatesHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetMessagesForMessageTemplatesResponseDto>>> Handle(GetMessagesForMessageTemplatesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessagesForMessageTemplatesAsync(request.MessageTemplatesName);
                return returnValue.ToResponse();
            }
        }
    }
}