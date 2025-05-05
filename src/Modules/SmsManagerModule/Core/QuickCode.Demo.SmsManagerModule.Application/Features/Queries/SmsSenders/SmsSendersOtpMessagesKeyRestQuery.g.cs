using System;
using System.Linq;
using MediatR;
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
    public class SmsSendersSmsSendersOtpMessagesKeyRestQuery : IRequest<Response<SmsSendersOtpMessagesKeyRestResponseDto>>
    {
        public int SmsSendersId { get; set; }
        public int OtpMessagesId { get; set; }

        public SmsSendersSmsSendersOtpMessagesKeyRestQuery(int smsSendersId, int otpMessagesId)
        {
            this.SmsSendersId = smsSendersId;
            this.OtpMessagesId = otpMessagesId;
        }

        public class SmsSendersSmsSendersOtpMessagesKeyRestHandler : IRequestHandler<SmsSendersSmsSendersOtpMessagesKeyRestQuery, Response<SmsSendersOtpMessagesKeyRestResponseDto>>
        {
            private readonly ILogger<SmsSendersSmsSendersOtpMessagesKeyRestHandler> _logger;
            private readonly ISmsSendersRepository _repository;
            public SmsSendersSmsSendersOtpMessagesKeyRestHandler(ILogger<SmsSendersSmsSendersOtpMessagesKeyRestHandler> logger, ISmsSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SmsSendersOtpMessagesKeyRestResponseDto>> Handle(SmsSendersSmsSendersOtpMessagesKeyRestQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SmsSendersOtpMessagesKeyRestAsync(request.SmsSendersId, request.OtpMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}