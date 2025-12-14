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
    public class ListBlackListQuery : IRequest<Response<List<BlackListDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ListBlackListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ListBlackListHandler : IRequestHandler<ListBlackListQuery, Response<List<BlackListDto>>>
        {
            private readonly ILogger<ListBlackListHandler> _logger;
            private readonly IBlackListRepository _repository;
            public ListBlackListHandler(ILogger<ListBlackListHandler> logger, IBlackListRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<BlackListDto>>> Handle(ListBlackListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}