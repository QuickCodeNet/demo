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
    public class EmailSendersEmailSendersOtpMessagesKeyRestQuery : IRequest<Response<EmailSendersOtpMessagesKeyRestResponseDto>>
    {
        public int EmailSendersId { get; set; }
        public int OtpMessagesId { get; set; }

        public EmailSendersEmailSendersOtpMessagesKeyRestQuery(int emailSendersId, int otpMessagesId)
        {
            this.EmailSendersId = emailSendersId;
            this.OtpMessagesId = otpMessagesId;
        }

        public class EmailSendersEmailSendersOtpMessagesKeyRestHandler : IRequestHandler<EmailSendersEmailSendersOtpMessagesKeyRestQuery, Response<EmailSendersOtpMessagesKeyRestResponseDto>>
        {
            private readonly ILogger<EmailSendersEmailSendersOtpMessagesKeyRestHandler> _logger;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersEmailSendersOtpMessagesKeyRestHandler(ILogger<EmailSendersEmailSendersOtpMessagesKeyRestHandler> logger, IEmailSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<EmailSendersOtpMessagesKeyRestResponseDto>> Handle(EmailSendersEmailSendersOtpMessagesKeyRestQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.EmailSendersOtpMessagesKeyRestAsync(request.EmailSendersId, request.OtpMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}