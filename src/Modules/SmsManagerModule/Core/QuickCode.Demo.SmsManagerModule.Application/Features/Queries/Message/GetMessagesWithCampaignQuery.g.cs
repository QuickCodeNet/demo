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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.Message;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.Message
{
    public class GetMessagesWithCampaignQuery : IRequest<Response<List<GetMessagesWithCampaignResponseDto>>>
    {
        public bool CampaignsIsActive { get; set; }
        public MessageStatus MessagesStatus { get; set; }

        public GetMessagesWithCampaignQuery(bool campaignsIsActive, MessageStatus messagesStatus)
        {
            this.CampaignsIsActive = campaignsIsActive;
            this.MessagesStatus = messagesStatus;
        }

        public class GetMessagesWithCampaignHandler : IRequestHandler<GetMessagesWithCampaignQuery, Response<List<GetMessagesWithCampaignResponseDto>>>
        {
            private readonly ILogger<GetMessagesWithCampaignHandler> _logger;
            private readonly IMessageRepository _repository;
            public GetMessagesWithCampaignHandler(ILogger<GetMessagesWithCampaignHandler> logger, IMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetMessagesWithCampaignResponseDto>>> Handle(GetMessagesWithCampaignQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessagesWithCampaignAsync(request.CampaignsIsActive, request.MessagesStatus);
                return returnValue.ToResponse();
            }
        }
    }
}