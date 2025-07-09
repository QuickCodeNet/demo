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
    public class CampaignTypesGetItemQuery : IRequest<Response<CampaignTypesDto>>
    {
        public int Id { get; set; }

        public CampaignTypesGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class CampaignTypesGetItemHandler : IRequestHandler<CampaignTypesGetItemQuery, Response<CampaignTypesDto>>
        {
            private readonly ILogger<CampaignTypesGetItemHandler> _logger;
            private readonly ICampaignTypesRepository _repository;
            public CampaignTypesGetItemHandler(ILogger<CampaignTypesGetItemHandler> logger, ICampaignTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CampaignTypesDto>> Handle(CampaignTypesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}