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
    public class GetItemMessageQueueQuery : IRequest<Response<MessageQueueDto>>
    {
        public int Id { get; set; }

        public GetItemMessageQueueQuery(int id)
        {
            this.Id = id;
        }

        public class GetItemMessageQueueHandler : IRequestHandler<GetItemMessageQueueQuery, Response<MessageQueueDto>>
        {
            private readonly ILogger<GetItemMessageQueueHandler> _logger;
            private readonly IMessageQueueRepository _repository;
            public GetItemMessageQueueHandler(ILogger<GetItemMessageQueueHandler> logger, IMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<MessageQueueDto>> Handle(GetItemMessageQueueQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}