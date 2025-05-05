using System;
using System.Linq;
using MediatR;
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
    public class EmailSendersEmailSendersInfoMessagesKeyRestQuery : IRequest<Response<EmailSendersInfoMessagesKeyRestResponseDto>>
    {
        public int EmailSendersId { get; set; }
        public int InfoMessagesId { get; set; }

        public EmailSendersEmailSendersInfoMessagesKeyRestQuery(int emailSendersId, int infoMessagesId)
        {
            this.EmailSendersId = emailSendersId;
            this.InfoMessagesId = infoMessagesId;
        }

        public class EmailSendersEmailSendersInfoMessagesKeyRestHandler : IRequestHandler<EmailSendersEmailSendersInfoMessagesKeyRestQuery, Response<EmailSendersInfoMessagesKeyRestResponseDto>>
        {
            private readonly ILogger<EmailSendersEmailSendersInfoMessagesKeyRestHandler> _logger;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersEmailSendersInfoMessagesKeyRestHandler(ILogger<EmailSendersEmailSendersInfoMessagesKeyRestHandler> logger, IEmailSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<EmailSendersInfoMessagesKeyRestResponseDto>> Handle(EmailSendersEmailSendersInfoMessagesKeyRestQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.EmailSendersInfoMessagesKeyRestAsync(request.EmailSendersId, request.InfoMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}