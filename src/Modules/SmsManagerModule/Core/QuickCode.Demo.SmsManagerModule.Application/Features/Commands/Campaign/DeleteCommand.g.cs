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
    public class DeleteCampaignCommand : IRequest<Response<bool>>
    {
        public CampaignDto request { get; set; }

        public DeleteCampaignCommand(CampaignDto request)
        {
            this.request = request;
        }

        public class DeleteCampaignHandler : IRequestHandler<DeleteCampaignCommand, Response<bool>>
        {
            private readonly ILogger<DeleteCampaignHandler> _logger;
            private readonly ICampaignRepository _repository;
            public DeleteCampaignHandler(ILogger<DeleteCampaignHandler> logger, ICampaignRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}