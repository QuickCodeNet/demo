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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessageLog;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessageLog
{
    public class GetOtpLogsWithMessageQuery : IRequest<Response<List<GetOtpLogsWithMessageResponseDto>>>
    {
        public MessageStatus OtpMessageLogsStatus { get; set; }

        public GetOtpLogsWithMessageQuery(MessageStatus otpMessageLogsStatus)
        {
            this.OtpMessageLogsStatus = otpMessageLogsStatus;
        }

        public class GetOtpLogsWithMessageHandler : IRequestHandler<GetOtpLogsWithMessageQuery, Response<List<GetOtpLogsWithMessageResponseDto>>>
        {
            private readonly ILogger<GetOtpLogsWithMessageHandler> _logger;
            private readonly IOtpMessageLogRepository _repository;
            public GetOtpLogsWithMessageHandler(ILogger<GetOtpLogsWithMessageHandler> logger, IOtpMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetOtpLogsWithMessageResponseDto>>> Handle(GetOtpLogsWithMessageQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetOtpLogsWithMessageAsync(request.OtpMessageLogsStatus);
                return returnValue.ToResponse();
            }
        }
    }
}