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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.Campaign;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.Campaign
{
    public class GetMessagesForCampaignsDetailsQuery : IRequest<Response<GetMessagesForCampaignsResponseDto>>
    {
        public int CampaignsId { get; set; }
        public int MessagesId { get; set; }

        public GetMessagesForCampaignsDetailsQuery(int campaignsId, int messagesId)
        {
            this.CampaignsId = campaignsId;
            this.MessagesId = messagesId;
        }

        public class GetMessagesForCampaignsDetailsHandler : IRequestHandler<GetMessagesForCampaignsDetailsQuery, Response<GetMessagesForCampaignsResponseDto>>
        {
            private readonly ILogger<GetMessagesForCampaignsDetailsHandler> _logger;
            private readonly ICampaignRepository _repository;
            public GetMessagesForCampaignsDetailsHandler(ILogger<GetMessagesForCampaignsDetailsHandler> logger, ICampaignRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetMessagesForCampaignsResponseDto>> Handle(GetMessagesForCampaignsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessagesForCampaignsDetailsAsync(request.CampaignsId, request.MessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}