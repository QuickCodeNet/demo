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
    public class GetByOtpMessageQuery : IRequest<Response<List<GetByOtpMessageResponseDto>>>
    {
        public int OtpMessageLogsOtpMessageId { get; set; }

        public GetByOtpMessageQuery(int otpMessageLogsOtpMessageId)
        {
            this.OtpMessageLogsOtpMessageId = otpMessageLogsOtpMessageId;
        }

        public class GetByOtpMessageHandler : IRequestHandler<GetByOtpMessageQuery, Response<List<GetByOtpMessageResponseDto>>>
        {
            private readonly ILogger<GetByOtpMessageHandler> _logger;
            private readonly IOtpMessageLogRepository _repository;
            public GetByOtpMessageHandler(ILogger<GetByOtpMessageHandler> logger, IOtpMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetByOtpMessageResponseDto>>> Handle(GetByOtpMessageQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByOtpMessageAsync(request.OtpMessageLogsOtpMessageId);
                return returnValue.ToResponse();
            }
        }
    }
}