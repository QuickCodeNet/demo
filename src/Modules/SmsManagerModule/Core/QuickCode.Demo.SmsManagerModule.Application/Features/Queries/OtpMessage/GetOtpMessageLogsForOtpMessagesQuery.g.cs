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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessage;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessage
{
    public class GetOtpMessageLogsForOtpMessagesQuery : IRequest<Response<List<GetOtpMessageLogsForOtpMessagesResponseDto>>>
    {
        public int OtpMessagesId { get; set; }

        public GetOtpMessageLogsForOtpMessagesQuery(int otpMessagesId)
        {
            this.OtpMessagesId = otpMessagesId;
        }

        public class GetOtpMessageLogsForOtpMessagesHandler : IRequestHandler<GetOtpMessageLogsForOtpMessagesQuery, Response<List<GetOtpMessageLogsForOtpMessagesResponseDto>>>
        {
            private readonly ILogger<GetOtpMessageLogsForOtpMessagesHandler> _logger;
            private readonly IOtpMessageRepository _repository;
            public GetOtpMessageLogsForOtpMessagesHandler(ILogger<GetOtpMessageLogsForOtpMessagesHandler> logger, IOtpMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetOtpMessageLogsForOtpMessagesResponseDto>>> Handle(GetOtpMessageLogsForOtpMessagesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetOtpMessageLogsForOtpMessagesAsync(request.OtpMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}