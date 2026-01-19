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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessageQueue;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessageQueue
{
    public class TotalCountOtpMessageQueueQuery : IRequest<Response<int>>
    {
        public TotalCountOtpMessageQueueQuery()
        {
        }

        public class TotalCountOtpMessageQueueHandler : IRequestHandler<TotalCountOtpMessageQueueQuery, Response<int>>
        {
            private readonly ILogger<TotalCountOtpMessageQueueHandler> _logger;
            private readonly IOtpMessageQueueRepository _repository;
            public TotalCountOtpMessageQueueHandler(ILogger<TotalCountOtpMessageQueueHandler> logger, IOtpMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountOtpMessageQueueQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}