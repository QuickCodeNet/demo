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
    public class CampaignMessagesTotalItemCountQuery : IRequest<Response<int>>
    {
        public CampaignMessagesTotalItemCountQuery()
        {
        }

        public class CampaignMessagesTotalItemCountHandler : IRequestHandler<CampaignMessagesTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<CampaignMessagesTotalItemCountHandler> _logger;
            private readonly ICampaignMessagesRepository _repository;
            public CampaignMessagesTotalItemCountHandler(ILogger<CampaignMessagesTotalItemCountHandler> logger, ICampaignMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(CampaignMessagesTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}