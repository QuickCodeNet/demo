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
    public class GetItemMessageLogQuery : IRequest<Response<MessageLogDto>>
    {
        public int Id { get; set; }

        public GetItemMessageLogQuery(int id)
        {
            this.Id = id;
        }

        public class GetItemMessageLogHandler : IRequestHandler<GetItemMessageLogQuery, Response<MessageLogDto>>
        {
            private readonly ILogger<GetItemMessageLogHandler> _logger;
            private readonly IMessageLogRepository _repository;
            public GetItemMessageLogHandler(ILogger<GetItemMessageLogHandler> logger, IMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<MessageLogDto>> Handle(GetItemMessageLogQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}