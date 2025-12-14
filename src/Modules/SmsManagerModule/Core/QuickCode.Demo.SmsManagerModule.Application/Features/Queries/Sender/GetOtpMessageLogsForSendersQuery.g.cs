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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.Sender;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.Sender
{
    public class GetOtpMessageLogsForSendersQuery : IRequest<Response<List<GetOtpMessageLogsForSendersResponseDto>>>
    {
        public int SendersId { get; set; }

        public GetOtpMessageLogsForSendersQuery(int sendersId)
        {
            this.SendersId = sendersId;
        }

        public class GetOtpMessageLogsForSendersHandler : IRequestHandler<GetOtpMessageLogsForSendersQuery, Response<List<GetOtpMessageLogsForSendersResponseDto>>>
        {
            private readonly ILogger<GetOtpMessageLogsForSendersHandler> _logger;
            private readonly ISenderRepository _repository;
            public GetOtpMessageLogsForSendersHandler(ILogger<GetOtpMessageLogsForSendersHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetOtpMessageLogsForSendersResponseDto>>> Handle(GetOtpMessageLogsForSendersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetOtpMessageLogsForSendersAsync(request.SendersId);
                return returnValue.ToResponse();
            }
        }
    }
}