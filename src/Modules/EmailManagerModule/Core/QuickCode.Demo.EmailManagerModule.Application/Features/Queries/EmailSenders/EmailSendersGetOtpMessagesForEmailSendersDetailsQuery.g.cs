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
    public class EmailSendersGetOtpMessagesForEmailSendersDetailsQuery : IRequest<Response<EmailSendersGetOtpMessagesForEmailSendersResponseDto>>
    {
        public int EmailSendersId { get; set; }
        public int OtpMessagesId { get; set; }

        public EmailSendersGetOtpMessagesForEmailSendersDetailsQuery(int emailSendersId, int otpMessagesId)
        {
            this.EmailSendersId = emailSendersId;
            this.OtpMessagesId = otpMessagesId;
        }

        public class EmailSendersGetOtpMessagesForEmailSendersDetailsHandler : IRequestHandler<EmailSendersGetOtpMessagesForEmailSendersDetailsQuery, Response<EmailSendersGetOtpMessagesForEmailSendersResponseDto>>
        {
            private readonly ILogger<EmailSendersGetOtpMessagesForEmailSendersDetailsHandler> _logger;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersGetOtpMessagesForEmailSendersDetailsHandler(ILogger<EmailSendersGetOtpMessagesForEmailSendersDetailsHandler> logger, IEmailSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<EmailSendersGetOtpMessagesForEmailSendersResponseDto>> Handle(EmailSendersGetOtpMessagesForEmailSendersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.EmailSendersGetOtpMessagesForEmailSendersDetailsAsync(request.EmailSendersId, request.OtpMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}