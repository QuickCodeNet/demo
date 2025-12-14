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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageTemplate;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.MessageTemplate
{
    public class GetCampaignsForMessageTemplatesDetailsQuery : IRequest<Response<GetCampaignsForMessageTemplatesResponseDto>>
    {
        public string MessageTemplatesName { get; set; }
        public int CampaignsId { get; set; }

        public GetCampaignsForMessageTemplatesDetailsQuery(string messageTemplatesName, int campaignsId)
        {
            this.MessageTemplatesName = messageTemplatesName;
            this.CampaignsId = campaignsId;
        }

        public class GetCampaignsForMessageTemplatesDetailsHandler : IRequestHandler<GetCampaignsForMessageTemplatesDetailsQuery, Response<GetCampaignsForMessageTemplatesResponseDto>>
        {
            private readonly ILogger<GetCampaignsForMessageTemplatesDetailsHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public GetCampaignsForMessageTemplatesDetailsHandler(ILogger<GetCampaignsForMessageTemplatesDetailsHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetCampaignsForMessageTemplatesResponseDto>> Handle(GetCampaignsForMessageTemplatesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetCampaignsForMessageTemplatesDetailsAsync(request.MessageTemplatesName, request.CampaignsId);
                return returnValue.ToResponse();
            }
        }
    }
}