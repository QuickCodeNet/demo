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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageLog;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.MessageLog
{
    public class GetLogsWithSenderQuery : IRequest<Response<List<GetLogsWithSenderResponseDto>>>
    {
        public bool SendersIsActive { get; set; }

        public GetLogsWithSenderQuery(bool sendersIsActive)
        {
            this.SendersIsActive = sendersIsActive;
        }

        public class GetLogsWithSenderHandler : IRequestHandler<GetLogsWithSenderQuery, Response<List<GetLogsWithSenderResponseDto>>>
        {
            private readonly ILogger<GetLogsWithSenderHandler> _logger;
            private readonly IMessageLogRepository _repository;
            public GetLogsWithSenderHandler(ILogger<GetLogsWithSenderHandler> logger, IMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetLogsWithSenderResponseDto>>> Handle(GetLogsWithSenderQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetLogsWithSenderAsync(request.SendersIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}