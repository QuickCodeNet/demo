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
    public class SmsSendersGetCampaignMessagesForSmsSendersQuery : IRequest<Response<List<SmsSendersGetCampaignMessagesForSmsSendersResponseDto>>>
    {
        public int SmsSendersId { get; set; }

        public SmsSendersGetCampaignMessagesForSmsSendersQuery(int smsSendersId)
        {
            this.SmsSendersId = smsSendersId;
        }

        public class SmsSendersGetCampaignMessagesForSmsSendersHandler : IRequestHandler<SmsSendersGetCampaignMessagesForSmsSendersQuery, Response<List<SmsSendersGetCampaignMessagesForSmsSendersResponseDto>>>
        {
            private readonly ILogger<SmsSendersGetCampaignMessagesForSmsSendersHandler> _logger;
            private readonly ISmsSendersRepository _repository;
            public SmsSendersGetCampaignMessagesForSmsSendersHandler(ILogger<SmsSendersGetCampaignMessagesForSmsSendersHandler> logger, ISmsSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SmsSendersGetCampaignMessagesForSmsSendersResponseDto>>> Handle(SmsSendersGetCampaignMessagesForSmsSendersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SmsSendersGetCampaignMessagesForSmsSendersAsync(request.SmsSendersId);
                return returnValue.ToResponse();
            }
        }
    }
}