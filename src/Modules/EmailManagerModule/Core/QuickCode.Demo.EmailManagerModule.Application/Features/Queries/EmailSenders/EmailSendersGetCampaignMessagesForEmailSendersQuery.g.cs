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
    public class EmailSendersGetCampaignMessagesForEmailSendersQuery : IRequest<Response<List<EmailSendersGetCampaignMessagesForEmailSendersResponseDto>>>
    {
        public int EmailSendersId { get; set; }

        public EmailSendersGetCampaignMessagesForEmailSendersQuery(int emailSendersId)
        {
            this.EmailSendersId = emailSendersId;
        }

        public class EmailSendersGetCampaignMessagesForEmailSendersHandler : IRequestHandler<EmailSendersGetCampaignMessagesForEmailSendersQuery, Response<List<EmailSendersGetCampaignMessagesForEmailSendersResponseDto>>>
        {
            private readonly ILogger<EmailSendersGetCampaignMessagesForEmailSendersHandler> _logger;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersGetCampaignMessagesForEmailSendersHandler(ILogger<EmailSendersGetCampaignMessagesForEmailSendersHandler> logger, IEmailSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<EmailSendersGetCampaignMessagesForEmailSendersResponseDto>>> Handle(EmailSendersGetCampaignMessagesForEmailSendersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.EmailSendersGetCampaignMessagesForEmailSendersAsync(request.EmailSendersId);
                return returnValue.ToResponse();
            }
        }
    }
}