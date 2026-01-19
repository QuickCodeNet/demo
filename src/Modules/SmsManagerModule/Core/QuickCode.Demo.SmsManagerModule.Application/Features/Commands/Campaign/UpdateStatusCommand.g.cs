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
    public class UpdateStatusCommand : IRequest<Response<int>>
    {
        public int CampaignsId { get; set; }
        public UpdateStatusRequestDto UpdateRequest { get; set; }

        public UpdateStatusCommand(int campaignsId, UpdateStatusRequestDto updateRequest)
        {
            this.CampaignsId = campaignsId;
            this.UpdateRequest = updateRequest;
        }

        public class UpdateStatusHandler : IRequestHandler<UpdateStatusCommand, Response<int>>
        {
            private readonly ILogger<UpdateStatusHandler> _logger;
            private readonly ICampaignRepository _repository;
            public UpdateStatusHandler(ILogger<UpdateStatusHandler> logger, ICampaignRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.UpdateStatusAsync(request.CampaignsId, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}