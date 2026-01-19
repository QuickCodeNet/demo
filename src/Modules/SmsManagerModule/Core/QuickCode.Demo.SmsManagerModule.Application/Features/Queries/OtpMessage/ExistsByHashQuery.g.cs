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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessage;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessage
{
    public class ExistsByHashQuery : IRequest<Response<bool>>
    {
        public string OtpMessagesHashCode { get; set; }

        public ExistsByHashQuery(string otpMessagesHashCode)
        {
            this.OtpMessagesHashCode = otpMessagesHashCode;
        }

        public class ExistsByHashHandler : IRequestHandler<ExistsByHashQuery, Response<bool>>
        {
            private readonly ILogger<ExistsByHashHandler> _logger;
            private readonly IOtpMessageRepository _repository;
            public ExistsByHashHandler(ILogger<ExistsByHashHandler> logger, IOtpMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ExistsByHashQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExistsByHashAsync(request.OtpMessagesHashCode);
                return returnValue.ToResponse();
            }
        }
    }
}