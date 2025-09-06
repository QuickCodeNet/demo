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
using QuickCode.Demo.SmsManagerModule.Application.Dtos;

namespace QuickCode.Demo.SmsManagerModule.Application.Features
{
    public class SmsSendersGetOtpMessagesForSmsSendersDetailsQuery : IRequest<Response<SmsSendersGetOtpMessagesForSmsSendersResponseDto>>
    {
        public int SmsSendersId { get; set; }
        public int OtpMessagesId { get; set; }

        public SmsSendersGetOtpMessagesForSmsSendersDetailsQuery(int smsSendersId, int otpMessagesId)
        {
            this.SmsSendersId = smsSendersId;
            this.OtpMessagesId = otpMessagesId;
        }

        public class SmsSendersGetOtpMessagesForSmsSendersDetailsHandler : IRequestHandler<SmsSendersGetOtpMessagesForSmsSendersDetailsQuery, Response<SmsSendersGetOtpMessagesForSmsSendersResponseDto>>
        {
            private readonly ILogger<SmsSendersGetOtpMessagesForSmsSendersDetailsHandler> _logger;
            private readonly ISmsSendersRepository _repository;
            public SmsSendersGetOtpMessagesForSmsSendersDetailsHandler(ILogger<SmsSendersGetOtpMessagesForSmsSendersDetailsHandler> logger, ISmsSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SmsSendersGetOtpMessagesForSmsSendersResponseDto>> Handle(SmsSendersGetOtpMessagesForSmsSendersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SmsSendersGetOtpMessagesForSmsSendersDetailsAsync(request.SmsSendersId, request.OtpMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}