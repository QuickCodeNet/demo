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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.Sender;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.Sender
{
    public class GetOtpMessageLogsForSendersDetailsQuery : IRequest<Response<GetOtpMessageLogsForSendersResponseDto>>
    {
        public int SendersId { get; set; }
        public int OtpMessageLogsId { get; set; }

        public GetOtpMessageLogsForSendersDetailsQuery(int sendersId, int otpMessageLogsId)
        {
            this.SendersId = sendersId;
            this.OtpMessageLogsId = otpMessageLogsId;
        }

        public class GetOtpMessageLogsForSendersDetailsHandler : IRequestHandler<GetOtpMessageLogsForSendersDetailsQuery, Response<GetOtpMessageLogsForSendersResponseDto>>
        {
            private readonly ILogger<GetOtpMessageLogsForSendersDetailsHandler> _logger;
            private readonly ISenderRepository _repository;
            public GetOtpMessageLogsForSendersDetailsHandler(ILogger<GetOtpMessageLogsForSendersDetailsHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetOtpMessageLogsForSendersResponseDto>> Handle(GetOtpMessageLogsForSendersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetOtpMessageLogsForSendersDetailsAsync(request.SendersId, request.OtpMessageLogsId);
                return returnValue.ToResponse();
            }
        }
    }
}