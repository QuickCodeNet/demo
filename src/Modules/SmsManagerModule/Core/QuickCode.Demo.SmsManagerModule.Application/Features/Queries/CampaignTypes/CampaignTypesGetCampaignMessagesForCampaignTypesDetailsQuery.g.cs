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
using QuickCode.Demo.SmsManagerModule.Application.Dtos;

namespace QuickCode.Demo.SmsManagerModule.Application.Features
{
    public class CampaignTypesGetCampaignMessagesForCampaignTypesDetailsQuery : IRequest<Response<CampaignTypesGetCampaignMessagesForCampaignTypesResponseDto>>
    {
        public int CampaignTypesId { get; set; }
        public int CampaignMessagesId { get; set; }

        public CampaignTypesGetCampaignMessagesForCampaignTypesDetailsQuery(int campaignTypesId, int campaignMessagesId)
        {
            this.CampaignTypesId = campaignTypesId;
            this.CampaignMessagesId = campaignMessagesId;
        }

        public class CampaignTypesGetCampaignMessagesForCampaignTypesDetailsHandler : IRequestHandler<CampaignTypesGetCampaignMessagesForCampaignTypesDetailsQuery, Response<CampaignTypesGetCampaignMessagesForCampaignTypesResponseDto>>
        {
            private readonly ILogger<CampaignTypesGetCampaignMessagesForCampaignTypesDetailsHandler> _logger;
            private readonly ICampaignTypesRepository _repository;
            public CampaignTypesGetCampaignMessagesForCampaignTypesDetailsHandler(ILogger<CampaignTypesGetCampaignMessagesForCampaignTypesDetailsHandler> logger, ICampaignTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CampaignTypesGetCampaignMessagesForCampaignTypesResponseDto>> Handle(CampaignTypesGetCampaignMessagesForCampaignTypesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CampaignTypesGetCampaignMessagesForCampaignTypesDetailsAsync(request.CampaignTypesId, request.CampaignMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}