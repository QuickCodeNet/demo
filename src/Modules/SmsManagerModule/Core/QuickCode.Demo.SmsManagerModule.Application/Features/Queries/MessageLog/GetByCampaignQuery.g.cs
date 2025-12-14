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
    public class GetByCampaignQuery : IRequest<Response<List<GetByCampaignResponseDto>>>
    {
        public int MessageLogsCampaignId { get; set; }

        public GetByCampaignQuery(int messageLogsCampaignId)
        {
            this.MessageLogsCampaignId = messageLogsCampaignId;
        }

        public class GetByCampaignHandler : IRequestHandler<GetByCampaignQuery, Response<List<GetByCampaignResponseDto>>>
        {
            private readonly ILogger<GetByCampaignHandler> _logger;
            private readonly IMessageLogRepository _repository;
            public GetByCampaignHandler(ILogger<GetByCampaignHandler> logger, IMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetByCampaignResponseDto>>> Handle(GetByCampaignQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByCampaignAsync(request.MessageLogsCampaignId);
                return returnValue.ToResponse();
            }
        }
    }
}