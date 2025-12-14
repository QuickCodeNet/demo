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
    public class GetByMessageQuery : IRequest<Response<List<GetByMessageResponseDto>>>
    {
        public int MessageLogsMessageId { get; set; }

        public GetByMessageQuery(int messageLogsMessageId)
        {
            this.MessageLogsMessageId = messageLogsMessageId;
        }

        public class GetByMessageHandler : IRequestHandler<GetByMessageQuery, Response<List<GetByMessageResponseDto>>>
        {
            private readonly ILogger<GetByMessageHandler> _logger;
            private readonly IMessageLogRepository _repository;
            public GetByMessageHandler(ILogger<GetByMessageHandler> logger, IMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetByMessageResponseDto>>> Handle(GetByMessageQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByMessageAsync(request.MessageLogsMessageId);
                return returnValue.ToResponse();
            }
        }
    }
}