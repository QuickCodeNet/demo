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
    public class InsertCampaignCommand : IRequest<Response<CampaignDto>>
    {
        public CampaignDto request { get; set; }

        public InsertCampaignCommand(CampaignDto request)
        {
            this.request = request;
        }

        public class InsertCampaignHandler : IRequestHandler<InsertCampaignCommand, Response<CampaignDto>>
        {
            private readonly ILogger<InsertCampaignHandler> _logger;
            private readonly ICampaignRepository _repository;
            public InsertCampaignHandler(ILogger<InsertCampaignHandler> logger, ICampaignRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CampaignDto>> Handle(InsertCampaignCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}