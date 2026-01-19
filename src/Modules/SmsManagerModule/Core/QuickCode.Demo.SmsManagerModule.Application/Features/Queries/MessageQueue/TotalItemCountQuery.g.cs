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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageQueue;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.MessageQueue
{
    public class TotalCountMessageQueueQuery : IRequest<Response<int>>
    {
        public TotalCountMessageQueueQuery()
        {
        }

        public class TotalCountMessageQueueHandler : IRequestHandler<TotalCountMessageQueueQuery, Response<int>>
        {
            private readonly ILogger<TotalCountMessageQueueHandler> _logger;
            private readonly IMessageQueueRepository _repository;
            public TotalCountMessageQueueHandler(ILogger<TotalCountMessageQueueHandler> logger, IMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountMessageQueueQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}