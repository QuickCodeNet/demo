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
    public class GetOtpMessageQueuesForSendersQuery : IRequest<Response<List<GetOtpMessageQueuesForSendersResponseDto>>>
    {
        public int SendersId { get; set; }

        public GetOtpMessageQueuesForSendersQuery(int sendersId)
        {
            this.SendersId = sendersId;
        }

        public class GetOtpMessageQueuesForSendersHandler : IRequestHandler<GetOtpMessageQueuesForSendersQuery, Response<List<GetOtpMessageQueuesForSendersResponseDto>>>
        {
            private readonly ILogger<GetOtpMessageQueuesForSendersHandler> _logger;
            private readonly ISenderRepository _repository;
            public GetOtpMessageQueuesForSendersHandler(ILogger<GetOtpMessageQueuesForSendersHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetOtpMessageQueuesForSendersResponseDto>>> Handle(GetOtpMessageQueuesForSendersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetOtpMessageQueuesForSendersAsync(request.SendersId);
                return returnValue.ToResponse();
            }
        }
    }
}