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
    public class GetActiveCampaignsQuery : IRequest<Response<List<GetActiveCampaignsResponseDto>>>
    {
        public bool CampaignsIsActive { get; set; }

        public GetActiveCampaignsQuery(bool campaignsIsActive)
        {
            this.CampaignsIsActive = campaignsIsActive;
        }

        public class GetActiveCampaignsHandler : IRequestHandler<GetActiveCampaignsQuery, Response<List<GetActiveCampaignsResponseDto>>>
        {
            private readonly ILogger<GetActiveCampaignsHandler> _logger;
            private readonly ICampaignRepository _repository;
            public GetActiveCampaignsHandler(ILogger<GetActiveCampaignsHandler> logger, ICampaignRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetActiveCampaignsResponseDto>>> Handle(GetActiveCampaignsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetActiveCampaignsAsync(request.CampaignsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}