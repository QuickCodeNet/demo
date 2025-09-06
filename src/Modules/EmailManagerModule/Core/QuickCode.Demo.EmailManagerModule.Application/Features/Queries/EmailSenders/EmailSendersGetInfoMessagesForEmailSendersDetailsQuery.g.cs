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
    public class EmailSendersGetInfoMessagesForEmailSendersDetailsQuery : IRequest<Response<EmailSendersGetInfoMessagesForEmailSendersResponseDto>>
    {
        public int EmailSendersId { get; set; }
        public int InfoMessagesId { get; set; }

        public EmailSendersGetInfoMessagesForEmailSendersDetailsQuery(int emailSendersId, int infoMessagesId)
        {
            this.EmailSendersId = emailSendersId;
            this.InfoMessagesId = infoMessagesId;
        }

        public class EmailSendersGetInfoMessagesForEmailSendersDetailsHandler : IRequestHandler<EmailSendersGetInfoMessagesForEmailSendersDetailsQuery, Response<EmailSendersGetInfoMessagesForEmailSendersResponseDto>>
        {
            private readonly ILogger<EmailSendersGetInfoMessagesForEmailSendersDetailsHandler> _logger;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersGetInfoMessagesForEmailSendersDetailsHandler(ILogger<EmailSendersGetInfoMessagesForEmailSendersDetailsHandler> logger, IEmailSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<EmailSendersGetInfoMessagesForEmailSendersResponseDto>> Handle(EmailSendersGetInfoMessagesForEmailSendersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.EmailSendersGetInfoMessagesForEmailSendersDetailsAsync(request.EmailSendersId, request.InfoMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}