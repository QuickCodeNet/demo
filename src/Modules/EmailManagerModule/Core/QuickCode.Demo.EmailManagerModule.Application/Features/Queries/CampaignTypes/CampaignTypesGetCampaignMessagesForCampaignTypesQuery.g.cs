using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos;

namespace QuickCode.Demo.EmailManagerModule.Application.Features
{
    public class CampaignTypesGetCampaignMessagesForCampaignTypesQuery : IRequest<Response<List<CampaignTypesGetCampaignMessagesForCampaignTypesResponseDto>>>
    {
        public int CampaignTypesId { get; set; }

        public CampaignTypesGetCampaignMessagesForCampaignTypesQuery(int campaignTypesId)
        {
            this.CampaignTypesId = campaignTypesId;
        }

        public class CampaignTypesGetCampaignMessagesForCampaignTypesHandler : IRequestHandler<CampaignTypesGetCampaignMessagesForCampaignTypesQuery, Response<List<CampaignTypesGetCampaignMessagesForCampaignTypesResponseDto>>>
        {
            private readonly ILogger<CampaignTypesGetCampaignMessagesForCampaignTypesHandler> _logger;
            private readonly ICampaignTypesRepository _repository;
            public CampaignTypesGetCampaignMessagesForCampaignTypesHandler(ILogger<CampaignTypesGetCampaignMessagesForCampaignTypesHandler> logger, ICampaignTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<CampaignTypesGetCampaignMessagesForCampaignTypesResponseDto>>> Handle(CampaignTypesGetCampaignMessagesForCampaignTypesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CampaignTypesGetCampaignMessagesForCampaignTypesAsync(request.CampaignTypesId);
                return returnValue.ToResponse();
            }
        }
    }
}