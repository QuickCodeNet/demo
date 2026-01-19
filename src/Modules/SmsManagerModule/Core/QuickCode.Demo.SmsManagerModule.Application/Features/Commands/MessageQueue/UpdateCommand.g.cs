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
    public class UpdateMessageQueueCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public MessageQueueDto request { get; set; }

        public UpdateMessageQueueCommand(int id, MessageQueueDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class UpdateMessageQueueHandler : IRequestHandler<UpdateMessageQueueCommand, Response<bool>>
        {
            private readonly ILogger<UpdateMessageQueueHandler> _logger;
            private readonly IMessageQueueRepository _repository;
            public UpdateMessageQueueHandler(ILogger<UpdateMessageQueueHandler> logger, IMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateMessageQueueCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}