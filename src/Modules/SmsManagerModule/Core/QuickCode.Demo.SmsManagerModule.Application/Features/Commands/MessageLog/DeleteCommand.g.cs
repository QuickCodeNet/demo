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
    public class DeleteMessageLogCommand : IRequest<Response<bool>>
    {
        public MessageLogDto request { get; set; }

        public DeleteMessageLogCommand(MessageLogDto request)
        {
            this.request = request;
        }

        public class DeleteMessageLogHandler : IRequestHandler<DeleteMessageLogCommand, Response<bool>>
        {
            private readonly ILogger<DeleteMessageLogHandler> _logger;
            private readonly IMessageLogRepository _repository;
            public DeleteMessageLogHandler(ILogger<DeleteMessageLogHandler> logger, IMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteMessageLogCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}