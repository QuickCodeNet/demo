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
    public class GetMessageQueuesForCampaignsQuery : IRequest<Response<List<GetMessageQueuesForCampaignsResponseDto>>>
    {
        public int CampaignsId { get; set; }

        public GetMessageQueuesForCampaignsQuery(int campaignsId)
        {
            this.CampaignsId = campaignsId;
        }

        public class GetMessageQueuesForCampaignsHandler : IRequestHandler<GetMessageQueuesForCampaignsQuery, Response<List<GetMessageQueuesForCampaignsResponseDto>>>
        {
            private readonly ILogger<GetMessageQueuesForCampaignsHandler> _logger;
            private readonly ICampaignRepository _repository;
            public GetMessageQueuesForCampaignsHandler(ILogger<GetMessageQueuesForCampaignsHandler> logger, ICampaignRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetMessageQueuesForCampaignsResponseDto>>> Handle(GetMessageQueuesForCampaignsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessageQueuesForCampaignsAsync(request.CampaignsId);
                return returnValue.ToResponse();
            }
        }
    }
}