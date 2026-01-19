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
    public class InsertOtpMessageCommand : IRequest<Response<OtpMessageDto>>
    {
        public OtpMessageDto request { get; set; }

        public InsertOtpMessageCommand(OtpMessageDto request)
        {
            this.request = request;
        }

        public class InsertOtpMessageHandler : IRequestHandler<InsertOtpMessageCommand, Response<OtpMessageDto>>
        {
            private readonly ILogger<InsertOtpMessageHandler> _logger;
            private readonly IOtpMessageRepository _repository;
            public InsertOtpMessageHandler(ILogger<InsertOtpMessageHandler> logger, IOtpMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<OtpMessageDto>> Handle(InsertOtpMessageCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}