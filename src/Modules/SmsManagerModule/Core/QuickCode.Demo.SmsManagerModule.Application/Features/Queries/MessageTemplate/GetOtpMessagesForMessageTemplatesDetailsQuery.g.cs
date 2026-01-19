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
    public class GetOtpMessagesForMessageTemplatesDetailsQuery : IRequest<Response<GetOtpMessagesForMessageTemplatesResponseDto>>
    {
        public string MessageTemplatesName { get; set; }
        public int OtpMessagesId { get; set; }

        public GetOtpMessagesForMessageTemplatesDetailsQuery(string messageTemplatesName, int otpMessagesId)
        {
            this.MessageTemplatesName = messageTemplatesName;
            this.OtpMessagesId = otpMessagesId;
        }

        public class GetOtpMessagesForMessageTemplatesDetailsHandler : IRequestHandler<GetOtpMessagesForMessageTemplatesDetailsQuery, Response<GetOtpMessagesForMessageTemplatesResponseDto>>
        {
            private readonly ILogger<GetOtpMessagesForMessageTemplatesDetailsHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public GetOtpMessagesForMessageTemplatesDetailsHandler(ILogger<GetOtpMessagesForMessageTemplatesDetailsHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetOtpMessagesForMessageTemplatesResponseDto>> Handle(GetOtpMessagesForMessageTemplatesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetOtpMessagesForMessageTemplatesDetailsAsync(request.MessageTemplatesName, request.OtpMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}