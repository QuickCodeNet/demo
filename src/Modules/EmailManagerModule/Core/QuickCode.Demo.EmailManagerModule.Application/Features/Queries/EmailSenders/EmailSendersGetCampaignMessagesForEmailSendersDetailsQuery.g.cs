using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos;

namespace QuickCode.Demo.EmailManagerModule.Application.Features
{
    public class EmailSendersGetCampaignMessagesForEmailSendersDetailsQuery : IRequest<Response<EmailSendersGetCampaignMessagesForEmailSendersResponseDto>>
    {
        public int EmailSendersId { get; set; }
        public int CampaignMessagesId { get; set; }

        public EmailSendersGetCampaignMessagesForEmailSendersDetailsQuery(int emailSendersId, int campaignMessagesId)
        {
            this.EmailSendersId = emailSendersId;
            this.CampaignMessagesId = campaignMessagesId;
        }

        public class EmailSendersGetCampaignMessagesForEmailSendersDetailsHandler : IRequestHandler<EmailSendersGetCampaignMessagesForEmailSendersDetailsQuery, Response<EmailSendersGetCampaignMessagesForEmailSendersResponseDto>>
        {
            private readonly ILogger<EmailSendersGetCampaignMessagesForEmailSendersDetailsHandler> _logger;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersGetCampaignMessagesForEmailSendersDetailsHandler(ILogger<EmailSendersGetCampaignMessagesForEmailSendersDetailsHandler> logger, IEmailSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<EmailSendersGetCampaignMessagesForEmailSendersResponseDto>> Handle(EmailSendersGetCampaignMessagesForEmailSendersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.EmailSendersGetCampaignMessagesForEmailSendersDetailsAsync(request.EmailSendersId, request.CampaignMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}