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
    public class DeleteOtpMessageLogCommand : IRequest<Response<bool>>
    {
        public OtpMessageLogDto request { get; set; }

        public DeleteOtpMessageLogCommand(OtpMessageLogDto request)
        {
            this.request = request;
        }

        public class DeleteOtpMessageLogHandler : IRequestHandler<DeleteOtpMessageLogCommand, Response<bool>>
        {
            private readonly ILogger<DeleteOtpMessageLogHandler> _logger;
            private readonly IOtpMessageLogRepository _repository;
            public DeleteOtpMessageLogHandler(ILogger<DeleteOtpMessageLogHandler> logger, IOtpMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteOtpMessageLogCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}