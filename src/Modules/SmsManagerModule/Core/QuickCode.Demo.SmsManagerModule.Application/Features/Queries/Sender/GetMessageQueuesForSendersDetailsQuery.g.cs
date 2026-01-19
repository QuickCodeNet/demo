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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.Sender;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.Sender
{
    public class GetMessageQueuesForSendersDetailsQuery : IRequest<Response<GetMessageQueuesForSendersResponseDto>>
    {
        public int SendersId { get; set; }
        public int MessageQueuesId { get; set; }

        public GetMessageQueuesForSendersDetailsQuery(int sendersId, int messageQueuesId)
        {
            this.SendersId = sendersId;
            this.MessageQueuesId = messageQueuesId;
        }

        public class GetMessageQueuesForSendersDetailsHandler : IRequestHandler<GetMessageQueuesForSendersDetailsQuery, Response<GetMessageQueuesForSendersResponseDto>>
        {
            private readonly ILogger<GetMessageQueuesForSendersDetailsHandler> _logger;
            private readonly ISenderRepository _repository;
            public GetMessageQueuesForSendersDetailsHandler(ILogger<GetMessageQueuesForSendersDetailsHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetMessageQueuesForSendersResponseDto>> Handle(GetMessageQueuesForSendersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessageQueuesForSendersDetailsAsync(request.SendersId, request.MessageQueuesId);
                return returnValue.ToResponse();
            }
        }
    }
}