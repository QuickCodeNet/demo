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
    public class GetCampaignsCountQuery : IRequest<Response<long>>
    {
        public bool CampaignsIsActive { get; set; }

        public GetCampaignsCountQuery(bool campaignsIsActive)
        {
            this.CampaignsIsActive = campaignsIsActive;
        }

        public class GetCampaignsCountHandler : IRequestHandler<GetCampaignsCountQuery, Response<long>>
        {
            private readonly ILogger<GetCampaignsCountHandler> _logger;
            private readonly ICampaignRepository _repository;
            public GetCampaignsCountHandler(ILogger<GetCampaignsCountHandler> logger, ICampaignRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(GetCampaignsCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetCampaignsCountAsync(request.CampaignsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}