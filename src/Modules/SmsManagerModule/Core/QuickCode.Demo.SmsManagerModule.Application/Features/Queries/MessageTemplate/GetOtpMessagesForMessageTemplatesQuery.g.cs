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
    public class GetOtpMessagesForMessageTemplatesQuery : IRequest<Response<List<GetOtpMessagesForMessageTemplatesResponseDto>>>
    {
        public string MessageTemplatesName { get; set; }

        public GetOtpMessagesForMessageTemplatesQuery(string messageTemplatesName)
        {
            this.MessageTemplatesName = messageTemplatesName;
        }

        public class GetOtpMessagesForMessageTemplatesHandler : IRequestHandler<GetOtpMessagesForMessageTemplatesQuery, Response<List<GetOtpMessagesForMessageTemplatesResponseDto>>>
        {
            private readonly ILogger<GetOtpMessagesForMessageTemplatesHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public GetOtpMessagesForMessageTemplatesHandler(ILogger<GetOtpMessagesForMessageTemplatesHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetOtpMessagesForMessageTemplatesResponseDto>>> Handle(GetOtpMessagesForMessageTemplatesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetOtpMessagesForMessageTemplatesAsync(request.MessageTemplatesName);
                return returnValue.ToResponse();
            }
        }
    }
}