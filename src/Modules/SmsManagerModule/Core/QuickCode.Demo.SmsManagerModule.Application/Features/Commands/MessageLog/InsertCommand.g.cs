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
    public class InsertMessageLogCommand : IRequest<Response<MessageLogDto>>
    {
        public MessageLogDto request { get; set; }

        public InsertMessageLogCommand(MessageLogDto request)
        {
            this.request = request;
        }

        public class InsertMessageLogHandler : IRequestHandler<InsertMessageLogCommand, Response<MessageLogDto>>
        {
            private readonly ILogger<InsertMessageLogHandler> _logger;
            private readonly IMessageLogRepository _repository;
            public InsertMessageLogHandler(ILogger<InsertMessageLogHandler> logger, IMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<MessageLogDto>> Handle(InsertMessageLogCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}