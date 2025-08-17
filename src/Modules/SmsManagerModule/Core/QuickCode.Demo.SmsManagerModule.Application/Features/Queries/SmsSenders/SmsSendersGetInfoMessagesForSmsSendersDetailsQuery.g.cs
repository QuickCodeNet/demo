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
using QuickCode.Demo.SmsManagerModule.Application.Dtos;

namespace QuickCode.Demo.SmsManagerModule.Application.Features
{
    public class SmsSendersGetInfoMessagesForSmsSendersDetailsQuery : IRequest<Response<SmsSendersGetInfoMessagesForSmsSendersResponseDto>>
    {
        public int SmsSendersId { get; set; }
        public int InfoMessagesId { get; set; }

        public SmsSendersGetInfoMessagesForSmsSendersDetailsQuery(int smsSendersId, int infoMessagesId)
        {
            this.SmsSendersId = smsSendersId;
            this.InfoMessagesId = infoMessagesId;
        }

        public class SmsSendersGetInfoMessagesForSmsSendersDetailsHandler : IRequestHandler<SmsSendersGetInfoMessagesForSmsSendersDetailsQuery, Response<SmsSendersGetInfoMessagesForSmsSendersResponseDto>>
        {
            private readonly ILogger<SmsSendersGetInfoMessagesForSmsSendersDetailsHandler> _logger;
            private readonly ISmsSendersRepository _repository;
            public SmsSendersGetInfoMessagesForSmsSendersDetailsHandler(ILogger<SmsSendersGetInfoMessagesForSmsSendersDetailsHandler> logger, ISmsSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SmsSendersGetInfoMessagesForSmsSendersResponseDto>> Handle(SmsSendersGetInfoMessagesForSmsSendersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SmsSendersGetInfoMessagesForSmsSendersDetailsAsync(request.SmsSendersId, request.InfoMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}