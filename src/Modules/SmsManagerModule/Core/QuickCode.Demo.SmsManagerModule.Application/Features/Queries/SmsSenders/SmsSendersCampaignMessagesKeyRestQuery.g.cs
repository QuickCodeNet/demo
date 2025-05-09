using System;
using System.Linq;
using MediatR;
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
    public class SmsSendersSmsSendersCampaignMessagesKeyRestQuery : IRequest<Response<SmsSendersCampaignMessagesKeyRestResponseDto>>
    {
        public int SmsSendersId { get; set; }
        public int CampaignMessagesId { get; set; }

        public SmsSendersSmsSendersCampaignMessagesKeyRestQuery(int smsSendersId, int campaignMessagesId)
        {
            this.SmsSendersId = smsSendersId;
            this.CampaignMessagesId = campaignMessagesId;
        }

        public class SmsSendersSmsSendersCampaignMessagesKeyRestHandler : IRequestHandler<SmsSendersSmsSendersCampaignMessagesKeyRestQuery, Response<SmsSendersCampaignMessagesKeyRestResponseDto>>
        {
            private readonly ILogger<SmsSendersSmsSendersCampaignMessagesKeyRestHandler> _logger;
            private readonly ISmsSendersRepository _repository;
            public SmsSendersSmsSendersCampaignMessagesKeyRestHandler(ILogger<SmsSendersSmsSendersCampaignMessagesKeyRestHandler> logger, ISmsSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SmsSendersCampaignMessagesKeyRestResponseDto>> Handle(SmsSendersSmsSendersCampaignMessagesKeyRestQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SmsSendersCampaignMessagesKeyRestAsync(request.SmsSendersId, request.CampaignMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}