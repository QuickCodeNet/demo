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
    public class TotalCountMessageLogQuery : IRequest<Response<int>>
    {
        public TotalCountMessageLogQuery()
        {
        }

        public class TotalCountMessageLogHandler : IRequestHandler<TotalCountMessageLogQuery, Response<int>>
        {
            private readonly ILogger<TotalCountMessageLogHandler> _logger;
            private readonly IMessageLogRepository _repository;
            public TotalCountMessageLogHandler(ILogger<TotalCountMessageLogHandler> logger, IMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountMessageLogQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}