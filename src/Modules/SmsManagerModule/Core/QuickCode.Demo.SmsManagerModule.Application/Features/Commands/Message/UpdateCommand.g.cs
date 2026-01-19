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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.Message;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.Message
{
    public class UpdateMessageCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public MessageDto request { get; set; }

        public UpdateMessageCommand(int id, MessageDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class UpdateMessageHandler : IRequestHandler<UpdateMessageCommand, Response<bool>>
        {
            private readonly ILogger<UpdateMessageHandler> _logger;
            private readonly IMessageRepository _repository;
            public UpdateMessageHandler(ILogger<UpdateMessageHandler> logger, IMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
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