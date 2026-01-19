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
    public class TotalCountBlackListQuery : IRequest<Response<int>>
    {
        public TotalCountBlackListQuery()
        {
        }

        public class TotalCountBlackListHandler : IRequestHandler<TotalCountBlackListQuery, Response<int>>
        {
            private readonly ILogger<TotalCountBlackListHandler> _logger;
            private readonly IBlackListRepository _repository;
            public TotalCountBlackListHandler(ILogger<TotalCountBlackListHandler> logger, IBlackListRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountBlackListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}