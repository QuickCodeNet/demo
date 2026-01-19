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
    public class GetOtpMessageQueuesForOtpMessagesQuery : IRequest<Response<List<GetOtpMessageQueuesForOtpMessagesResponseDto>>>
    {
        public int OtpMessagesId { get; set; }

        public GetOtpMessageQueuesForOtpMessagesQuery(int otpMessagesId)
        {
            this.OtpMessagesId = otpMessagesId;
        }

        public class GetOtpMessageQueuesForOtpMessagesHandler : IRequestHandler<GetOtpMessageQueuesForOtpMessagesQuery, Response<List<GetOtpMessageQueuesForOtpMessagesResponseDto>>>
        {
            private readonly ILogger<GetOtpMessageQueuesForOtpMessagesHandler> _logger;
            private readonly IOtpMessageRepository _repository;
            public GetOtpMessageQueuesForOtpMessagesHandler(ILogger<GetOtpMessageQueuesForOtpMessagesHandler> logger, IOtpMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetOtpMessageQueuesForOtpMessagesResponseDto>>> Handle(GetOtpMessageQueuesForOtpMessagesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetOtpMessageQueuesForOtpMessagesAsync(request.OtpMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}