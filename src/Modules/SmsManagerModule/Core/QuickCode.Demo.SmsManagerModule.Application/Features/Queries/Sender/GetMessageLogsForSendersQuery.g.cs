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
    public class GetMessageLogsForSendersQuery : IRequest<Response<List<GetMessageLogsForSendersResponseDto>>>
    {
        public int SendersId { get; set; }

        public GetMessageLogsForSendersQuery(int sendersId)
        {
            this.SendersId = sendersId;
        }

        public class GetMessageLogsForSendersHandler : IRequestHandler<GetMessageLogsForSendersQuery, Response<List<GetMessageLogsForSendersResponseDto>>>
        {
            private readonly ILogger<GetMessageLogsForSendersHandler> _logger;
            private readonly ISenderRepository _repository;
            public GetMessageLogsForSendersHandler(ILogger<GetMessageLogsForSendersHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetMessageLogsForSendersResponseDto>>> Handle(GetMessageLogsForSendersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessageLogsForSendersAsync(request.SendersId);
                return returnValue.ToResponse();
            }
        }
    }
}