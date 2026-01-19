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
    public class GetOtpMessageLogsForOtpMessagesDetailsQuery : IRequest<Response<GetOtpMessageLogsForOtpMessagesResponseDto>>
    {
        public int OtpMessagesId { get; set; }
        public int OtpMessageLogsId { get; set; }

        public GetOtpMessageLogsForOtpMessagesDetailsQuery(int otpMessagesId, int otpMessageLogsId)
        {
            this.OtpMessagesId = otpMessagesId;
            this.OtpMessageLogsId = otpMessageLogsId;
        }

        public class GetOtpMessageLogsForOtpMessagesDetailsHandler : IRequestHandler<GetOtpMessageLogsForOtpMessagesDetailsQuery, Response<GetOtpMessageLogsForOtpMessagesResponseDto>>
        {
            private readonly ILogger<GetOtpMessageLogsForOtpMessagesDetailsHandler> _logger;
            private readonly IOtpMessageRepository _repository;
            public GetOtpMessageLogsForOtpMessagesDetailsHandler(ILogger<GetOtpMessageLogsForOtpMessagesDetailsHandler> logger, IOtpMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetOtpMessageLogsForOtpMessagesResponseDto>> Handle(GetOtpMessageLogsForOtpMessagesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetOtpMessageLogsForOtpMessagesDetailsAsync(request.OtpMessagesId, request.OtpMessageLogsId);
                return returnValue.ToResponse();
            }
        }
    }
}