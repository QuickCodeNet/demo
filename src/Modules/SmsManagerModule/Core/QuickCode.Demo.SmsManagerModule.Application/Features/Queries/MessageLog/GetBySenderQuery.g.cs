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
    public class GetBySenderQuery : IRequest<Response<List<GetBySenderResponseDto>>>
    {
        public int? MessageLogsSenderId { get; set; }

        public GetBySenderQuery(int? messageLogsSenderId)
        {
            this.MessageLogsSenderId = messageLogsSenderId;
        }

        public class GetBySenderHandler : IRequestHandler<GetBySenderQuery, Response<List<GetBySenderResponseDto>>>
        {
            private readonly ILogger<GetBySenderHandler> _logger;
            private readonly IMessageLogRepository _repository;
            public GetBySenderHandler(ILogger<GetBySenderHandler> logger, IMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetBySenderResponseDto>>> Handle(GetBySenderQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetBySenderAsync(request.MessageLogsSenderId);
                return returnValue.ToResponse();
            }
        }
    }
}