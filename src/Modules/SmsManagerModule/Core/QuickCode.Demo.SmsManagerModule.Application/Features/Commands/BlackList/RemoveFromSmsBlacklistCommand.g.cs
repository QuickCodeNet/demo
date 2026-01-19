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
    public class RemoveFromSmsBlacklistCommand : IRequest<Response<int>>
    {
        public BlacklistReasonType BlackListsReasonType { get; set; }

        public RemoveFromSmsBlacklistCommand(BlacklistReasonType blackListsReasonType)
        {
            this.BlackListsReasonType = blackListsReasonType;
        }

        public class RemoveFromSmsBlacklistHandler : IRequestHandler<RemoveFromSmsBlacklistCommand, Response<int>>
        {
            private readonly ILogger<RemoveFromSmsBlacklistHandler> _logger;
            private readonly IBlackListRepository _repository;
            public RemoveFromSmsBlacklistHandler(ILogger<RemoveFromSmsBlacklistHandler> logger, IBlackListRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(RemoveFromSmsBlacklistCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.RemoveFromSmsBlacklistAsync(request.BlackListsReasonType);
                return returnValue.ToResponse();
            }
        }
    }
}