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
    public class GetByStatusQuery : IRequest<Response<List<GetByStatusResponseDto>>>
    {
        public MessageStatus MessagesStatus { get; set; }

        public GetByStatusQuery(MessageStatus messagesStatus)
        {
            this.MessagesStatus = messagesStatus;
        }

        public class GetByStatusHandler : IRequestHandler<GetByStatusQuery, Response<List<GetByStatusResponseDto>>>
        {
            private readonly ILogger<GetByStatusHandler> _logger;
            private readonly IMessageRepository _repository;
            public GetByStatusHandler(ILogger<GetByStatusHandler> logger, IMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetByStatusResponseDto>>> Handle(GetByStatusQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByStatusAsync(request.MessagesStatus);
                return returnValue.ToResponse();
            }
        }
    }
}