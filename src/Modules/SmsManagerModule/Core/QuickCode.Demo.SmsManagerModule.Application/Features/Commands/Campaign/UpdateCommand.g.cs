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
    public class UpdateCampaignCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public CampaignDto request { get; set; }

        public UpdateCampaignCommand(int id, CampaignDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class UpdateCampaignHandler : IRequestHandler<UpdateCampaignCommand, Response<bool>>
        {
            private readonly ILogger<UpdateCampaignHandler> _logger;
            private readonly ICampaignRepository _repository;
            public UpdateCampaignHandler(ILogger<UpdateCampaignHandler> logger, ICampaignRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}