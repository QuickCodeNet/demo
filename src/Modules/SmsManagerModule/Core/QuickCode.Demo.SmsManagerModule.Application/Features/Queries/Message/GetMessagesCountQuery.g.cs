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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.Message;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.Message
{
    public class GetMessagesCountQuery : IRequest<Response<long>>
    {
        public int MessagesCampaignId { get; set; }

        public GetMessagesCountQuery(int messagesCampaignId)
        {
            this.MessagesCampaignId = messagesCampaignId;
        }

        public class GetMessagesCountHandler : IRequestHandler<GetMessagesCountQuery, Response<long>>
        {
            private readonly ILogger<GetMessagesCountHandler> _logger;
            private readonly IMessageRepository _repository;
            public GetMessagesCountHandler(ILogger<GetMessagesCountHandler> logger, IMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(GetMessagesCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessagesCountAsync(request.MessagesCampaignId);
                return returnValue.ToResponse();
            }
        }
    }
}