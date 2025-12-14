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
    public class InsertMessageCommand : IRequest<Response<MessageDto>>
    {
        public MessageDto request { get; set; }

        public InsertMessageCommand(MessageDto request)
        {
            this.request = request;
        }

        public class InsertMessageHandler : IRequestHandler<InsertMessageCommand, Response<MessageDto>>
        {
            private readonly ILogger<InsertMessageHandler> _logger;
            private readonly IMessageRepository _repository;
            public InsertMessageHandler(ILogger<InsertMessageHandler> logger, IMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<MessageDto>> Handle(InsertMessageCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}