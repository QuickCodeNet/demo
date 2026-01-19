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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessage;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessage
{
    public class GetItemOtpMessageQuery : IRequest<Response<OtpMessageDto>>
    {
        public int Id { get; set; }

        public GetItemOtpMessageQuery(int id)
        {
            this.Id = id;
        }

        public class GetItemOtpMessageHandler : IRequestHandler<GetItemOtpMessageQuery, Response<OtpMessageDto>>
        {
            private readonly ILogger<GetItemOtpMessageHandler> _logger;
            private readonly IOtpMessageRepository _repository;
            public GetItemOtpMessageHandler(ILogger<GetItemOtpMessageHandler> logger, IOtpMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<OtpMessageDto>> Handle(GetItemOtpMessageQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}