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
    public class GetItemCampaignQuery : IRequest<Response<CampaignDto>>
    {
        public int Id { get; set; }

        public GetItemCampaignQuery(int id)
        {
            this.Id = id;
        }

        public class GetItemCampaignHandler : IRequestHandler<GetItemCampaignQuery, Response<CampaignDto>>
        {
            private readonly ILogger<GetItemCampaignHandler> _logger;
            private readonly ICampaignRepository _repository;
            public GetItemCampaignHandler(ILogger<GetItemCampaignHandler> logger, ICampaignRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CampaignDto>> Handle(GetItemCampaignQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}