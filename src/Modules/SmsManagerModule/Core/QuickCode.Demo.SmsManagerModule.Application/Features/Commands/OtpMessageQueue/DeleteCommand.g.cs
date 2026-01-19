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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessageQueue;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessageQueue
{
    public class DeleteOtpMessageQueueCommand : IRequest<Response<bool>>
    {
        public OtpMessageQueueDto request { get; set; }

        public DeleteOtpMessageQueueCommand(OtpMessageQueueDto request)
        {
            this.request = request;
        }

        public class DeleteOtpMessageQueueHandler : IRequestHandler<DeleteOtpMessageQueueCommand, Response<bool>>
        {
            private readonly ILogger<DeleteOtpMessageQueueHandler> _logger;
            private readonly IOtpMessageQueueRepository _repository;
            public DeleteOtpMessageQueueHandler(ILogger<DeleteOtpMessageQueueHandler> logger, IOtpMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteOtpMessageQueueCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}