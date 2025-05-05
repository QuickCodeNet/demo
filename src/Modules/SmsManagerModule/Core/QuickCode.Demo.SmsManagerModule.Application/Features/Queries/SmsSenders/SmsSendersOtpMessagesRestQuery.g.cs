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
    public class SmsSendersSmsSendersOtpMessagesRestQuery : IRequest<Response<List<SmsSendersOtpMessagesRestResponseDto>>>
    {
        public int SmsSendersId { get; set; }

        public SmsSendersSmsSendersOtpMessagesRestQuery(int smsSendersId)
        {
            this.SmsSendersId = smsSendersId;
        }

        public class SmsSendersSmsSendersOtpMessagesRestHandler : IRequestHandler<SmsSendersSmsSendersOtpMessagesRestQuery, Response<List<SmsSendersOtpMessagesRestResponseDto>>>
        {
            private readonly ILogger<SmsSendersSmsSendersOtpMessagesRestHandler> _logger;
            private readonly ISmsSendersRepository _repository;
            public SmsSendersSmsSendersOtpMessagesRestHandler(ILogger<SmsSendersSmsSendersOtpMessagesRestHandler> logger, ISmsSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SmsSendersOtpMessagesRestResponseDto>>> Handle(SmsSendersSmsSendersOtpMessagesRestQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SmsSendersOtpMessagesRestAsync(request.SmsSendersId);
                return returnValue.ToResponse();
            }
        }
    }
}