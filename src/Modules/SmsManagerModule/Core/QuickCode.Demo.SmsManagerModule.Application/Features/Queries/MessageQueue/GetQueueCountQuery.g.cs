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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageQueue;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.MessageQueue
{
    public class GetQueueCountQuery : IRequest<Response<long>>
    {
        public int MessageQueuesCampaignId { get; set; }

        public GetQueueCountQuery(int messageQueuesCampaignId)
        {
            this.MessageQueuesCampaignId = messageQueuesCampaignId;
        }

        public class GetQueueCountHandler : IRequestHandler<GetQueueCountQuery, Response<long>>
        {
            private readonly ILogger<GetQueueCountHandler> _logger;
            private readonly IMessageQueueRepository _repository;
            public GetQueueCountHandler(ILogger<GetQueueCountHandler> logger, IMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(GetQueueCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetQueueCountAsync(request.MessageQueuesCampaignId);
                return returnValue.ToResponse();
            }
        }
    }
}