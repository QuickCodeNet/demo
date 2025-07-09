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
using QuickCode.Demo.SmsManagerModule.Application.Dtos;

namespace QuickCode.Demo.SmsManagerModule.Application.Features
{
    public class SmsSendersGetCampaignMessagesForSmsSendersDetailsQuery : IRequest<Response<SmsSendersGetCampaignMessagesForSmsSendersResponseDto>>
    {
        public int SmsSendersId { get; set; }
        public int CampaignMessagesId { get; set; }

        public SmsSendersGetCampaignMessagesForSmsSendersDetailsQuery(int smsSendersId, int campaignMessagesId)
        {
            this.SmsSendersId = smsSendersId;
            this.CampaignMessagesId = campaignMessagesId;
        }

        public class SmsSendersGetCampaignMessagesForSmsSendersDetailsHandler : IRequestHandler<SmsSendersGetCampaignMessagesForSmsSendersDetailsQuery, Response<SmsSendersGetCampaignMessagesForSmsSendersResponseDto>>
        {
            private readonly ILogger<SmsSendersGetCampaignMessagesForSmsSendersDetailsHandler> _logger;
            private readonly ISmsSendersRepository _repository;
            public SmsSendersGetCampaignMessagesForSmsSendersDetailsHandler(ILogger<SmsSendersGetCampaignMessagesForSmsSendersDetailsHandler> logger, ISmsSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SmsSendersGetCampaignMessagesForSmsSendersResponseDto>> Handle(SmsSendersGetCampaignMessagesForSmsSendersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SmsSendersGetCampaignMessagesForSmsSendersDetailsAsync(request.SmsSendersId, request.CampaignMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}