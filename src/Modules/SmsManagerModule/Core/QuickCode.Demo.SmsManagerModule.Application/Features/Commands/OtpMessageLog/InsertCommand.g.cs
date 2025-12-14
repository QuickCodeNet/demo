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
    public class InsertOtpMessageLogCommand : IRequest<Response<OtpMessageLogDto>>
    {
        public OtpMessageLogDto request { get; set; }

        public InsertOtpMessageLogCommand(OtpMessageLogDto request)
        {
            this.request = request;
        }

        public class InsertOtpMessageLogHandler : IRequestHandler<InsertOtpMessageLogCommand, Response<OtpMessageLogDto>>
        {
            private readonly ILogger<InsertOtpMessageLogHandler> _logger;
            private readonly IOtpMessageLogRepository _repository;
            public InsertOtpMessageLogHandler(ILogger<InsertOtpMessageLogHandler> logger, IOtpMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<OtpMessageLogDto>> Handle(InsertOtpMessageLogCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}