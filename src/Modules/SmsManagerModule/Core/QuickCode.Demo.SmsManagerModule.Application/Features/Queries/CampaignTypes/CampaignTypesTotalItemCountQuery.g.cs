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
    public class CampaignTypesTotalItemCountQuery : IRequest<Response<int>>
    {
        public CampaignTypesTotalItemCountQuery()
        {
        }

        public class CampaignTypesTotalItemCountHandler : IRequestHandler<CampaignTypesTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<CampaignTypesTotalItemCountHandler> _logger;
            private readonly ICampaignTypesRepository _repository;
            public CampaignTypesTotalItemCountHandler(ILogger<CampaignTypesTotalItemCountHandler> logger, ICampaignTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(CampaignTypesTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}