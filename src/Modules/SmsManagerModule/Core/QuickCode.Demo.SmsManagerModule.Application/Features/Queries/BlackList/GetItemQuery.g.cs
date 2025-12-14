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
    public class GetItemBlackListQuery : IRequest<Response<BlackListDto>>
    {
        public int Id { get; set; }

        public GetItemBlackListQuery(int id)
        {
            this.Id = id;
        }

        public class GetItemBlackListHandler : IRequestHandler<GetItemBlackListQuery, Response<BlackListDto>>
        {
            private readonly ILogger<GetItemBlackListHandler> _logger;
            private readonly IBlackListRepository _repository;
            public GetItemBlackListHandler(ILogger<GetItemBlackListHandler> logger, IBlackListRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<BlackListDto>> Handle(GetItemBlackListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}