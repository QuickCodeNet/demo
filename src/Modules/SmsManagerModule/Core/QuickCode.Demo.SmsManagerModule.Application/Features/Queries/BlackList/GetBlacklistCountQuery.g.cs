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
    public class GetBlacklistCountQuery : IRequest<Response<long>>
    {
        public BlacklistReasonType BlackListsReasonType { get; set; }

        public GetBlacklistCountQuery(BlacklistReasonType blackListsReasonType)
        {
            this.BlackListsReasonType = blackListsReasonType;
        }

        public class GetBlacklistCountHandler : IRequestHandler<GetBlacklistCountQuery, Response<long>>
        {
            private readonly ILogger<GetBlacklistCountHandler> _logger;
            private readonly IBlackListRepository _repository;
            public GetBlacklistCountHandler(ILogger<GetBlacklistCountHandler> logger, IBlackListRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(GetBlacklistCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetBlacklistCountAsync(request.BlackListsReasonType);
                return returnValue.ToResponse();
            }
        }
    }
}