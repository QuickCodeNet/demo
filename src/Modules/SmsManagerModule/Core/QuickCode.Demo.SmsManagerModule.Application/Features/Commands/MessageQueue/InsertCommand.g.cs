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
    public class InsertMessageQueueCommand : IRequest<Response<MessageQueueDto>>
    {
        public MessageQueueDto request { get; set; }

        public InsertMessageQueueCommand(MessageQueueDto request)
        {
            this.request = request;
        }

        public class InsertMessageQueueHandler : IRequestHandler<InsertMessageQueueCommand, Response<MessageQueueDto>>
        {
            private readonly ILogger<InsertMessageQueueHandler> _logger;
            private readonly IMessageQueueRepository _repository;
            public InsertMessageQueueHandler(ILogger<InsertMessageQueueHandler> logger, IMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<MessageQueueDto>> Handle(InsertMessageQueueCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}