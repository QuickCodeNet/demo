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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.BlackList;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.BlackList
{
    public class GetByRecipientQuery : IRequest<Response<GetByRecipientResponseDto>>
    {
        public string BlackListsRecipient { get; set; }

        public GetByRecipientQuery(string blackListsRecipient)
        {
            this.BlackListsRecipient = blackListsRecipient;
        }

        public class GetByRecipientHandler : IRequestHandler<GetByRecipientQuery, Response<GetByRecipientResponseDto>>
        {
            private readonly ILogger<GetByRecipientHandler> _logger;
            private readonly IBlackListRepository _repository;
            public GetByRecipientHandler(ILogger<GetByRecipientHandler> logger, IBlackListRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetByRecipientResponseDto>> Handle(GetByRecipientQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByRecipientAsync(request.BlackListsRecipient);
                return returnValue.ToResponse();
            }
        }
    }
}