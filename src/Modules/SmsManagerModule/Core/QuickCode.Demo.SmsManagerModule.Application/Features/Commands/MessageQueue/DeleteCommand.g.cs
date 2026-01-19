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
    public class DeleteMessageQueueCommand : IRequest<Response<bool>>
    {
        public MessageQueueDto request { get; set; }

        public DeleteMessageQueueCommand(MessageQueueDto request)
        {
            this.request = request;
        }

        public class DeleteMessageQueueHandler : IRequestHandler<DeleteMessageQueueCommand, Response<bool>>
        {
            private readonly ILogger<DeleteMessageQueueHandler> _logger;
            private readonly IMessageQueueRepository _repository;
            public DeleteMessageQueueHandler(ILogger<DeleteMessageQueueHandler> logger, IMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteMessageQueueCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}