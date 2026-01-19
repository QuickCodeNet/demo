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
    public class GetOtpMessageQueuesForSendersDetailsQuery : IRequest<Response<GetOtpMessageQueuesForSendersResponseDto>>
    {
        public int SendersId { get; set; }
        public int OtpMessageQueuesId { get; set; }

        public GetOtpMessageQueuesForSendersDetailsQuery(int sendersId, int otpMessageQueuesId)
        {
            this.SendersId = sendersId;
            this.OtpMessageQueuesId = otpMessageQueuesId;
        }

        public class GetOtpMessageQueuesForSendersDetailsHandler : IRequestHandler<GetOtpMessageQueuesForSendersDetailsQuery, Response<GetOtpMessageQueuesForSendersResponseDto>>
        {
            private readonly ILogger<GetOtpMessageQueuesForSendersDetailsHandler> _logger;
            private readonly ISenderRepository _repository;
            public GetOtpMessageQueuesForSendersDetailsHandler(ILogger<GetOtpMessageQueuesForSendersDetailsHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetOtpMessageQueuesForSendersResponseDto>> Handle(GetOtpMessageQueuesForSendersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetOtpMessageQueuesForSendersDetailsAsync(request.SendersId, request.OtpMessageQueuesId);
                return returnValue.ToResponse();
            }
        }
    }
}