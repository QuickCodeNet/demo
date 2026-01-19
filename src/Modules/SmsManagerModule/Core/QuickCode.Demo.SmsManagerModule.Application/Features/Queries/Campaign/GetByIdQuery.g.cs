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
    public class GetByIdQuery : IRequest<Response<GetByIdResponseDto>>
    {
        public int CampaignsId { get; set; }

        public GetByIdQuery(int campaignsId)
        {
            this.CampaignsId = campaignsId;
        }

        public class GetByIdHandler : IRequestHandler<GetByIdQuery, Response<GetByIdResponseDto>>
        {
            private readonly ILogger<GetByIdHandler> _logger;
            private readonly ICampaignRepository _repository;
            public GetByIdHandler(ILogger<GetByIdHandler> logger, ICampaignRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetByIdResponseDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByIdAsync(request.CampaignsId);
                return returnValue.ToResponse();
            }
        }
    }
}