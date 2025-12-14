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
    public class ExistsByNameQuery : IRequest<Response<bool>>
    {
        public string CampaignsName { get; set; }

        public ExistsByNameQuery(string campaignsName)
        {
            this.CampaignsName = campaignsName;
        }

        public class ExistsByNameHandler : IRequestHandler<ExistsByNameQuery, Response<bool>>
        {
            private readonly ILogger<ExistsByNameHandler> _logger;
            private readonly ICampaignRepository _repository;
            public ExistsByNameHandler(ILogger<ExistsByNameHandler> logger, ICampaignRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ExistsByNameQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExistsByNameAsync(request.CampaignsName);
                return returnValue.ToResponse();
            }
        }
    }
}