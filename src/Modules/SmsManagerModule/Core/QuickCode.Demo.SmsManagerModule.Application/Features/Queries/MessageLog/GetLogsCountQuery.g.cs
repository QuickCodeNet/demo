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
    public class GetLogsCountQuery : IRequest<Response<long>>
    {
        public int MessageLogsCampaignId { get; set; }

        public GetLogsCountQuery(int messageLogsCampaignId)
        {
            this.MessageLogsCampaignId = messageLogsCampaignId;
        }

        public class GetLogsCountHandler : IRequestHandler<GetLogsCountQuery, Response<long>>
        {
            private readonly ILogger<GetLogsCountHandler> _logger;
            private readonly IMessageLogRepository _repository;
            public GetLogsCountHandler(ILogger<GetLogsCountHandler> logger, IMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(GetLogsCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetLogsCountAsync(request.MessageLogsCampaignId);
                return returnValue.ToResponse();
            }
        }
    }
}