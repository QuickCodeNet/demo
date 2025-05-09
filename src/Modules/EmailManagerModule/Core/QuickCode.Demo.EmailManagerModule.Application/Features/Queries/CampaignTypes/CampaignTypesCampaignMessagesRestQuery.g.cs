using System;
using System.Linq;
using MediatR;
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
    public class CampaignTypesCampaignTypesCampaignMessagesRestQuery : IRequest<Response<List<CampaignTypesCampaignMessagesRestResponseDto>>>
    {
        public int CampaignTypesId { get; set; }

        public CampaignTypesCampaignTypesCampaignMessagesRestQuery(int campaignTypesId)
        {
            this.CampaignTypesId = campaignTypesId;
        }

        public class CampaignTypesCampaignTypesCampaignMessagesRestHandler : IRequestHandler<CampaignTypesCampaignTypesCampaignMessagesRestQuery, Response<List<CampaignTypesCampaignMessagesRestResponseDto>>>
        {
            private readonly ILogger<CampaignTypesCampaignTypesCampaignMessagesRestHandler> _logger;
            private readonly ICampaignTypesRepository _repository;
            public CampaignTypesCampaignTypesCampaignMessagesRestHandler(ILogger<CampaignTypesCampaignTypesCampaignMessagesRestHandler> logger, ICampaignTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<CampaignTypesCampaignMessagesRestResponseDto>>> Handle(CampaignTypesCampaignTypesCampaignMessagesRestQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CampaignTypesCampaignMessagesRestAsync(request.CampaignTypesId);
                return returnValue.ToResponse();
            }
        }
    }
}