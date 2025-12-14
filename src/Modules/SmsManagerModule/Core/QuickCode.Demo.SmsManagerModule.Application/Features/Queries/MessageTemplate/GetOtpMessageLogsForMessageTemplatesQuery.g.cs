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
    public class GetOtpMessageLogsForMessageTemplatesQuery : IRequest<Response<List<GetOtpMessageLogsForMessageTemplatesResponseDto>>>
    {
        public string MessageTemplatesName { get; set; }

        public GetOtpMessageLogsForMessageTemplatesQuery(string messageTemplatesName)
        {
            this.MessageTemplatesName = messageTemplatesName;
        }

        public class GetOtpMessageLogsForMessageTemplatesHandler : IRequestHandler<GetOtpMessageLogsForMessageTemplatesQuery, Response<List<GetOtpMessageLogsForMessageTemplatesResponseDto>>>
        {
            private readonly ILogger<GetOtpMessageLogsForMessageTemplatesHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public GetOtpMessageLogsForMessageTemplatesHandler(ILogger<GetOtpMessageLogsForMessageTemplatesHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetOtpMessageLogsForMessageTemplatesResponseDto>>> Handle(GetOtpMessageLogsForMessageTemplatesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetOtpMessageLogsForMessageTemplatesAsync(request.MessageTemplatesName);
                return returnValue.ToResponse();
            }
        }
    }
}