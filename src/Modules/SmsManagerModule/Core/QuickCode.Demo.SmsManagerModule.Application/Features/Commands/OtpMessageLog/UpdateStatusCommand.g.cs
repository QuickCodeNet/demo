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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessageLog;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessageLog
{
    public class UpdateStatusCommand : IRequest<Response<int>>
    {
        public int OtpMessageLogsId { get; set; }
        public UpdateStatusRequestDto UpdateRequest { get; set; }

        public UpdateStatusCommand(int otpMessageLogsId, UpdateStatusRequestDto updateRequest)
        {
            this.OtpMessageLogsId = otpMessageLogsId;
            this.UpdateRequest = updateRequest;
        }

        public class UpdateStatusHandler : IRequestHandler<UpdateStatusCommand, Response<int>>
        {
            private readonly ILogger<UpdateStatusHandler> _logger;
            private readonly IOtpMessageLogRepository _repository;
            public UpdateStatusHandler(ILogger<UpdateStatusHandler> logger, IOtpMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.UpdateStatusAsync(request.OtpMessageLogsId, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}