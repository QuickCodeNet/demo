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
    public class GetByRecipientQuery : IRequest<Response<List<GetByRecipientResponseDto>>>
    {
        public string OtpMessagesRecipient { get; set; }

        public GetByRecipientQuery(string otpMessagesRecipient)
        {
            this.OtpMessagesRecipient = otpMessagesRecipient;
        }

        public class GetByRecipientHandler : IRequestHandler<GetByRecipientQuery, Response<List<GetByRecipientResponseDto>>>
        {
            private readonly ILogger<GetByRecipientHandler> _logger;
            private readonly IOtpMessageRepository _repository;
            public GetByRecipientHandler(ILogger<GetByRecipientHandler> logger, IOtpMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetByRecipientResponseDto>>> Handle(GetByRecipientQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByRecipientAsync(request.OtpMessagesRecipient);
                return returnValue.ToResponse();
            }
        }
    }
}