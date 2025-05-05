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
    public class SmsSendersSmsSendersCampaignMessagesRestQuery : IRequest<Response<List<SmsSendersCampaignMessagesRestResponseDto>>>
    {
        public int SmsSendersId { get; set; }

        public SmsSendersSmsSendersCampaignMessagesRestQuery(int smsSendersId)
        {
            this.SmsSendersId = smsSendersId;
        }

        public class SmsSendersSmsSendersCampaignMessagesRestHandler : IRequestHandler<SmsSendersSmsSendersCampaignMessagesRestQuery, Response<List<SmsSendersCampaignMessagesRestResponseDto>>>
        {
            private readonly ILogger<SmsSendersSmsSendersCampaignMessagesRestHandler> _logger;
            private readonly ISmsSendersRepository _repository;
            public SmsSendersSmsSendersCampaignMessagesRestHandler(ILogger<SmsSendersSmsSendersCampaignMessagesRestHandler> logger, ISmsSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SmsSendersCampaignMessagesRestResponseDto>>> Handle(SmsSendersSmsSendersCampaignMessagesRestQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SmsSendersCampaignMessagesRestAsync(request.SmsSendersId);
                return returnValue.ToResponse();
            }
        }
    }
}