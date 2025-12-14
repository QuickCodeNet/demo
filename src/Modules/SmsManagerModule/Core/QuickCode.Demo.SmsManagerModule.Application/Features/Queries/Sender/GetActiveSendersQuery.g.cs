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
    public class GetActiveSendersQuery : IRequest<Response<List<GetActiveSendersResponseDto>>>
    {
        public bool SendersIsActive { get; set; }

        public GetActiveSendersQuery(bool sendersIsActive)
        {
            this.SendersIsActive = sendersIsActive;
        }

        public class GetActiveSendersHandler : IRequestHandler<GetActiveSendersQuery, Response<List<GetActiveSendersResponseDto>>>
        {
            private readonly ILogger<GetActiveSendersHandler> _logger;
            private readonly ISenderRepository _repository;
            public GetActiveSendersHandler(ILogger<GetActiveSendersHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetActiveSendersResponseDto>>> Handle(GetActiveSendersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetActiveSendersAsync(request.SendersIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}