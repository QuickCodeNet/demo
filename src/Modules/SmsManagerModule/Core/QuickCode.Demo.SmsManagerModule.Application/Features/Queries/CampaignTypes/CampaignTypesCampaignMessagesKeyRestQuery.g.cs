using System;
using System.Linq;
using MediatR;
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
    public class CampaignTypesCampaignTypesCampaignMessagesKeyRestQuery : IRequest<Response<CampaignTypesCampaignMessagesKeyRestResponseDto>>
    {
        public int CampaignTypesId { get; set; }
        public int CampaignMessagesId { get; set; }

        public CampaignTypesCampaignTypesCampaignMessagesKeyRestQuery(int campaignTypesId, int campaignMessagesId)
        {
            this.CampaignTypesId = campaignTypesId;
            this.CampaignMessagesId = campaignMessagesId;
        }

        public class CampaignTypesCampaignTypesCampaignMessagesKeyRestHandler : IRequestHandler<CampaignTypesCampaignTypesCampaignMessagesKeyRestQuery, Response<CampaignTypesCampaignMessagesKeyRestResponseDto>>
        {
            private readonly ILogger<CampaignTypesCampaignTypesCampaignMessagesKeyRestHandler> _logger;
            private readonly ICampaignTypesRepository _repository;
            public CampaignTypesCampaignTypesCampaignMessagesKeyRestHandler(ILogger<CampaignTypesCampaignTypesCampaignMessagesKeyRestHandler> logger, ICampaignTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CampaignTypesCampaignMessagesKeyRestResponseDto>> Handle(CampaignTypesCampaignTypesCampaignMessagesKeyRestQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CampaignTypesCampaignMessagesKeyRestAsync(request.CampaignTypesId, request.CampaignMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}