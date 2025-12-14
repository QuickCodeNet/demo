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
    public class TotalCountCampaignQuery : IRequest<Response<int>>
    {
        public TotalCountCampaignQuery()
        {
        }

        public class TotalCountCampaignHandler : IRequestHandler<TotalCountCampaignQuery, Response<int>>
        {
            private readonly ILogger<TotalCountCampaignHandler> _logger;
            private readonly ICampaignRepository _repository;
            public TotalCountCampaignHandler(ILogger<TotalCountCampaignHandler> logger, ICampaignRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountCampaignQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}