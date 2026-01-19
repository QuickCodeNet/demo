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
    public class GetOtpMessageLogsForMessageTemplatesDetailsQuery : IRequest<Response<GetOtpMessageLogsForMessageTemplatesResponseDto>>
    {
        public string MessageTemplatesName { get; set; }
        public int OtpMessageLogsId { get; set; }

        public GetOtpMessageLogsForMessageTemplatesDetailsQuery(string messageTemplatesName, int otpMessageLogsId)
        {
            this.MessageTemplatesName = messageTemplatesName;
            this.OtpMessageLogsId = otpMessageLogsId;
        }

        public class GetOtpMessageLogsForMessageTemplatesDetailsHandler : IRequestHandler<GetOtpMessageLogsForMessageTemplatesDetailsQuery, Response<GetOtpMessageLogsForMessageTemplatesResponseDto>>
        {
            private readonly ILogger<GetOtpMessageLogsForMessageTemplatesDetailsHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public GetOtpMessageLogsForMessageTemplatesDetailsHandler(ILogger<GetOtpMessageLogsForMessageTemplatesDetailsHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetOtpMessageLogsForMessageTemplatesResponseDto>> Handle(GetOtpMessageLogsForMessageTemplatesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetOtpMessageLogsForMessageTemplatesDetailsAsync(request.MessageTemplatesName, request.OtpMessageLogsId);
                return returnValue.ToResponse();
            }
        }
    }
}