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
    public class DeleteOtpMessageCommand : IRequest<Response<bool>>
    {
        public OtpMessageDto request { get; set; }

        public DeleteOtpMessageCommand(OtpMessageDto request)
        {
            this.request = request;
        }

        public class DeleteOtpMessageHandler : IRequestHandler<DeleteOtpMessageCommand, Response<bool>>
        {
            private readonly ILogger<DeleteOtpMessageHandler> _logger;
            private readonly IOtpMessageRepository _repository;
            public DeleteOtpMessageHandler(ILogger<DeleteOtpMessageHandler> logger, IOtpMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteOtpMessageCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}