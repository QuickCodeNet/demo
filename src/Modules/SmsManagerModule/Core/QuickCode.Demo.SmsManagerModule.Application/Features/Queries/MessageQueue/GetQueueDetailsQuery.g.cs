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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageQueue;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.MessageQueue
{
    public class GetQueueDetailsQuery : IRequest<Response<List<GetQueueDetailsResponseDto>>>
    {
        public MessageStatus MessageQueuesStatus { get; set; }
        public bool CampaignsIsActive { get; set; }

        public GetQueueDetailsQuery(MessageStatus messageQueuesStatus, bool campaignsIsActive)
        {
            this.MessageQueuesStatus = messageQueuesStatus;
            this.CampaignsIsActive = campaignsIsActive;
        }

        public class GetQueueDetailsHandler : IRequestHandler<GetQueueDetailsQuery, Response<List<GetQueueDetailsResponseDto>>>
        {
            private readonly ILogger<GetQueueDetailsHandler> _logger;
            private readonly IMessageQueueRepository _repository;
            public GetQueueDetailsHandler(ILogger<GetQueueDetailsHandler> logger, IMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetQueueDetailsResponseDto>>> Handle(GetQueueDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetQueueDetailsAsync(request.MessageQueuesStatus, request.CampaignsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}