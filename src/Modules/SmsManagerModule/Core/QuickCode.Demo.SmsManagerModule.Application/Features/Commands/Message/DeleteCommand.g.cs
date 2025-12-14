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
    public class DeleteMessageCommand : IRequest<Response<bool>>
    {
        public MessageDto request { get; set; }

        public DeleteMessageCommand(MessageDto request)
        {
            this.request = request;
        }

        public class DeleteMessageHandler : IRequestHandler<DeleteMessageCommand, Response<bool>>
        {
            private readonly ILogger<DeleteMessageHandler> _logger;
            private readonly IMessageRepository _repository;
            public DeleteMessageHandler(ILogger<DeleteMessageHandler> logger, IMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}