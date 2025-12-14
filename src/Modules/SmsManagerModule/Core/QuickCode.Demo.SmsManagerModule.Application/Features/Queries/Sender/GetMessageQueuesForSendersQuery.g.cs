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
    public class GetMessageQueuesForSendersQuery : IRequest<Response<List<GetMessageQueuesForSendersResponseDto>>>
    {
        public int SendersId { get; set; }

        public GetMessageQueuesForSendersQuery(int sendersId)
        {
            this.SendersId = sendersId;
        }

        public class GetMessageQueuesForSendersHandler : IRequestHandler<GetMessageQueuesForSendersQuery, Response<List<GetMessageQueuesForSendersResponseDto>>>
        {
            private readonly ILogger<GetMessageQueuesForSendersHandler> _logger;
            private readonly ISenderRepository _repository;
            public GetMessageQueuesForSendersHandler(ILogger<GetMessageQueuesForSendersHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetMessageQueuesForSendersResponseDto>>> Handle(GetMessageQueuesForSendersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetMessageQueuesForSendersAsync(request.SendersId);
                return returnValue.ToResponse();
            }
        }
    }
}