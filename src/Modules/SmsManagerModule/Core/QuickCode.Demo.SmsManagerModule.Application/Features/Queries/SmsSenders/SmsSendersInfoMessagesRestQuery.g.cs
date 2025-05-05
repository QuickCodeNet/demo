using System;
using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SmsManagerModule.Application.Dtos;

namespace QuickCode.Demo.SmsManagerModule.Application.Features
{
    public class SmsSendersSmsSendersInfoMessagesRestQuery : IRequest<Response<List<SmsSendersInfoMessagesRestResponseDto>>>
    {
        public int SmsSendersId { get; set; }

        public SmsSendersSmsSendersInfoMessagesRestQuery(int smsSendersId)
        {
            this.SmsSendersId = smsSendersId;
        }

        public class SmsSendersSmsSendersInfoMessagesRestHandler : IRequestHandler<SmsSendersSmsSendersInfoMessagesRestQuery, Response<List<SmsSendersInfoMessagesRestResponseDto>>>
        {
            private readonly ILogger<SmsSendersSmsSendersInfoMessagesRestHandler> _logger;
            private readonly ISmsSendersRepository _repository;
            public SmsSendersSmsSendersInfoMessagesRestHandler(ILogger<SmsSendersSmsSendersInfoMessagesRestHandler> logger, ISmsSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SmsSendersInfoMessagesRestResponseDto>>> Handle(SmsSendersSmsSendersInfoMessagesRestQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SmsSendersInfoMessagesRestAsync(request.SmsSendersId);
                return returnValue.ToResponse();
            }
        }
    }
}