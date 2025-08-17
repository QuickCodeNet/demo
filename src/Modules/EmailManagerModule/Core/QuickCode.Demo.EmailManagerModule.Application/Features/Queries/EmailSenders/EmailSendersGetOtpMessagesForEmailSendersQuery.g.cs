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
    public class EmailSendersGetOtpMessagesForEmailSendersQuery : IRequest<Response<List<EmailSendersGetOtpMessagesForEmailSendersResponseDto>>>
    {
        public int EmailSendersId { get; set; }

        public EmailSendersGetOtpMessagesForEmailSendersQuery(int emailSendersId)
        {
            this.EmailSendersId = emailSendersId;
        }

        public class EmailSendersGetOtpMessagesForEmailSendersHandler : IRequestHandler<EmailSendersGetOtpMessagesForEmailSendersQuery, Response<List<EmailSendersGetOtpMessagesForEmailSendersResponseDto>>>
        {
            private readonly ILogger<EmailSendersGetOtpMessagesForEmailSendersHandler> _logger;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersGetOtpMessagesForEmailSendersHandler(ILogger<EmailSendersGetOtpMessagesForEmailSendersHandler> logger, IEmailSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<EmailSendersGetOtpMessagesForEmailSendersResponseDto>>> Handle(EmailSendersGetOtpMessagesForEmailSendersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.EmailSendersGetOtpMessagesForEmailSendersAsync(request.EmailSendersId);
                return returnValue.ToResponse();
            }
        }
    }
}