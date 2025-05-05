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
    public class SmsSendersSmsSendersInfoMessagesKeyRestQuery : IRequest<Response<SmsSendersInfoMessagesKeyRestResponseDto>>
    {
        public int SmsSendersId { get; set; }
        public int InfoMessagesId { get; set; }

        public SmsSendersSmsSendersInfoMessagesKeyRestQuery(int smsSendersId, int infoMessagesId)
        {
            this.SmsSendersId = smsSendersId;
            this.InfoMessagesId = infoMessagesId;
        }

        public class SmsSendersSmsSendersInfoMessagesKeyRestHandler : IRequestHandler<SmsSendersSmsSendersInfoMessagesKeyRestQuery, Response<SmsSendersInfoMessagesKeyRestResponseDto>>
        {
            private readonly ILogger<SmsSendersSmsSendersInfoMessagesKeyRestHandler> _logger;
            private readonly ISmsSendersRepository _repository;
            public SmsSendersSmsSendersInfoMessagesKeyRestHandler(ILogger<SmsSendersSmsSendersInfoMessagesKeyRestHandler> logger, ISmsSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SmsSendersInfoMessagesKeyRestResponseDto>> Handle(SmsSendersSmsSendersInfoMessagesKeyRestQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SmsSendersInfoMessagesKeyRestAsync(request.SmsSendersId, request.InfoMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}