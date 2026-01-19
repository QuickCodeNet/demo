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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageLog;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.MessageLog
{
    public class GetByIdQuery : IRequest<Response<GetByIdResponseDto>>
    {
        public int MessageLogsId { get; set; }

        public GetByIdQuery(int messageLogsId)
        {
            this.MessageLogsId = messageLogsId;
        }

        public class GetByIdHandler : IRequestHandler<GetByIdQuery, Response<GetByIdResponseDto>>
        {
            private readonly ILogger<GetByIdHandler> _logger;
            private readonly IMessageLogRepository _repository;
            public GetByIdHandler(ILogger<GetByIdHandler> logger, IMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetByIdResponseDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByIdAsync(request.MessageLogsId);
                return returnValue.ToResponse();
            }
        }
    }
}