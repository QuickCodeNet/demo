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
    public class CampaignTypesListQuery : IRequest<Response<List<CampaignTypesDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public CampaignTypesListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class CampaignTypesListHandler : IRequestHandler<CampaignTypesListQuery, Response<List<CampaignTypesDto>>>
        {
            private readonly ILogger<CampaignTypesListHandler> _logger;
            private readonly ICampaignTypesRepository _repository;
            public CampaignTypesListHandler(ILogger<CampaignTypesListHandler> logger, ICampaignTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<CampaignTypesDto>>> Handle(CampaignTypesListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}