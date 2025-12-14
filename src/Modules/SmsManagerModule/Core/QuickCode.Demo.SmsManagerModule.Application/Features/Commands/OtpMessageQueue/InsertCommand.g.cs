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
    public class InsertOtpMessageQueueCommand : IRequest<Response<OtpMessageQueueDto>>
    {
        public OtpMessageQueueDto request { get; set; }

        public InsertOtpMessageQueueCommand(OtpMessageQueueDto request)
        {
            this.request = request;
        }

        public class InsertOtpMessageQueueHandler : IRequestHandler<InsertOtpMessageQueueCommand, Response<OtpMessageQueueDto>>
        {
            private readonly ILogger<InsertOtpMessageQueueHandler> _logger;
            private readonly IOtpMessageQueueRepository _repository;
            public InsertOtpMessageQueueHandler(ILogger<InsertOtpMessageQueueHandler> logger, IOtpMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<OtpMessageQueueDto>> Handle(InsertOtpMessageQueueCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}