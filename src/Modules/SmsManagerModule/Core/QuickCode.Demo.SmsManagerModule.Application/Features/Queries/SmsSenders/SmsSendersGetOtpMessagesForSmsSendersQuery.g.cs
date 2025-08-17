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
    public class SmsSendersGetOtpMessagesForSmsSendersQuery : IRequest<Response<List<SmsSendersGetOtpMessagesForSmsSendersResponseDto>>>
    {
        public int SmsSendersId { get; set; }

        public SmsSendersGetOtpMessagesForSmsSendersQuery(int smsSendersId)
        {
            this.SmsSendersId = smsSendersId;
        }

        public class SmsSendersGetOtpMessagesForSmsSendersHandler : IRequestHandler<SmsSendersGetOtpMessagesForSmsSendersQuery, Response<List<SmsSendersGetOtpMessagesForSmsSendersResponseDto>>>
        {
            private readonly ILogger<SmsSendersGetOtpMessagesForSmsSendersHandler> _logger;
            private readonly ISmsSendersRepository _repository;
            public SmsSendersGetOtpMessagesForSmsSendersHandler(ILogger<SmsSendersGetOtpMessagesForSmsSendersHandler> logger, ISmsSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SmsSendersGetOtpMessagesForSmsSendersResponseDto>>> Handle(SmsSendersGetOtpMessagesForSmsSendersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SmsSendersGetOtpMessagesForSmsSendersAsync(request.SmsSendersId);
                return returnValue.ToResponse();
            }
        }
    }
}