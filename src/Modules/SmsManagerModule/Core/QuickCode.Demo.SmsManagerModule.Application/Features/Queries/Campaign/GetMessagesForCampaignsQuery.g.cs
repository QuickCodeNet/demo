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
    public class GetMessagesForCampaignsQuery : IRequest<Response<List<GetMessagesForCampaignsResponseDto>>>
    {
        public int CampaignsId { get; set; }

        public GetMessagesForCampaignsQuery(int campaignsId)
        {
            this.CampaignsId = campaignsId;
        }

        public class GetMessagesForCampaignsHandler : IRequestHandler<GetMessagesForCampaignsQuery, Response<List<GetMessagesForCampaignsResponseDto>>>
        {
            private readonly ILogger<GetMessagesForCampaignsHandler> _logger;
            private readonly ICampaignRepository _repository;
            public GetMessagesForCampaignsHandler(ILogger<GetMessagesForCampaignsHandler> logger, ICampaignRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetMessagesForCampaignsResponseDto>>> Handle(GetMessagesForCampaignsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessagesForCampaignsAsync(request.CampaignsId);
                return returnValue.ToResponse();
            }
        }
    }
}