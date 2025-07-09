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
    public class CampaignTypesInsertCommand : IRequest<Response<CampaignTypesDto>>
    {
        public CampaignTypesDto request { get; set; }

        public CampaignTypesInsertCommand(CampaignTypesDto request)
        {
            this.request = request;
        }

        public class CampaignTypesInsertHandler : IRequestHandler<CampaignTypesInsertCommand, Response<CampaignTypesDto>>
        {
            private readonly ILogger<CampaignTypesInsertHandler> _logger;
            private readonly ICampaignTypesRepository _repository;
            public CampaignTypesInsertHandler(ILogger<CampaignTypesInsertHandler> logger, ICampaignTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CampaignTypesDto>> Handle(CampaignTypesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}