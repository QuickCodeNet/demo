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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessageQueue;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessageQueue
{
    public class GetItemOtpMessageQueueQuery : IRequest<Response<OtpMessageQueueDto>>
    {
        public int Id { get; set; }

        public GetItemOtpMessageQueueQuery(int id)
        {
            this.Id = id;
        }

        public class GetItemOtpMessageQueueHandler : IRequestHandler<GetItemOtpMessageQueueQuery, Response<OtpMessageQueueDto>>
        {
            private readonly ILogger<GetItemOtpMessageQueueHandler> _logger;
            private readonly IOtpMessageQueueRepository _repository;
            public GetItemOtpMessageQueueHandler(ILogger<GetItemOtpMessageQueueHandler> logger, IOtpMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<OtpMessageQueueDto>> Handle(GetItemOtpMessageQueueQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}