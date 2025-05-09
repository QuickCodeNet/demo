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
    public class EmailSendersEmailSendersOtpMessagesRestQuery : IRequest<Response<List<EmailSendersOtpMessagesRestResponseDto>>>
    {
        public int EmailSendersId { get; set; }

        public EmailSendersEmailSendersOtpMessagesRestQuery(int emailSendersId)
        {
            this.EmailSendersId = emailSendersId;
        }

        public class EmailSendersEmailSendersOtpMessagesRestHandler : IRequestHandler<EmailSendersEmailSendersOtpMessagesRestQuery, Response<List<EmailSendersOtpMessagesRestResponseDto>>>
        {
            private readonly ILogger<EmailSendersEmailSendersOtpMessagesRestHandler> _logger;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersEmailSendersOtpMessagesRestHandler(ILogger<EmailSendersEmailSendersOtpMessagesRestHandler> logger, IEmailSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<EmailSendersOtpMessagesRestResponseDto>>> Handle(EmailSendersEmailSendersOtpMessagesRestQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.EmailSendersOtpMessagesRestAsync(request.EmailSendersId);
                return returnValue.ToResponse();
            }
        }
    }
}