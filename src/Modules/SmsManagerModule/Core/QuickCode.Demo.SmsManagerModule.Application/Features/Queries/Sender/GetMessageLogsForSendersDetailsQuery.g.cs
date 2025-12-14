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
    public class GetMessageLogsForSendersDetailsQuery : IRequest<Response<GetMessageLogsForSendersResponseDto>>
    {
        public int SendersId { get; set; }
        public int MessageLogsId { get; set; }

        public GetMessageLogsForSendersDetailsQuery(int sendersId, int messageLogsId)
        {
            this.SendersId = sendersId;
            this.MessageLogsId = messageLogsId;
        }

        public class GetMessageLogsForSendersDetailsHandler : IRequestHandler<GetMessageLogsForSendersDetailsQuery, Response<GetMessageLogsForSendersResponseDto>>
        {
            private readonly ILogger<GetMessageLogsForSendersDetailsHandler> _logger;
            private readonly ISenderRepository _repository;
            public GetMessageLogsForSendersDetailsHandler(ILogger<GetMessageLogsForSendersDetailsHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetMessageLogsForSendersResponseDto>> Handle(GetMessageLogsForSendersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessageLogsForSendersDetailsAsync(request.SendersId, request.MessageLogsId);
                return returnValue.ToResponse();
            }
        }
    }
}