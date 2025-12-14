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
    public class GetCampaignsForMessageTemplatesQuery : IRequest<Response<List<GetCampaignsForMessageTemplatesResponseDto>>>
    {
        public string MessageTemplatesName { get; set; }

        public GetCampaignsForMessageTemplatesQuery(string messageTemplatesName)
        {
            this.MessageTemplatesName = messageTemplatesName;
        }

        public class GetCampaignsForMessageTemplatesHandler : IRequestHandler<GetCampaignsForMessageTemplatesQuery, Response<List<GetCampaignsForMessageTemplatesResponseDto>>>
        {
            private readonly ILogger<GetCampaignsForMessageTemplatesHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public GetCampaignsForMessageTemplatesHandler(ILogger<GetCampaignsForMessageTemplatesHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetCampaignsForMessageTemplatesResponseDto>>> Handle(GetCampaignsForMessageTemplatesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetCampaignsForMessageTemplatesAsync(request.MessageTemplatesName);
                return returnValue.ToResponse();
            }
        }
    }
}