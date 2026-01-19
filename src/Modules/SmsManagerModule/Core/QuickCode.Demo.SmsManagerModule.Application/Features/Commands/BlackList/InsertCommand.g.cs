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
    public class InsertBlackListCommand : IRequest<Response<BlackListDto>>
    {
        public BlackListDto request { get; set; }

        public InsertBlackListCommand(BlackListDto request)
        {
            this.request = request;
        }

        public class InsertBlackListHandler : IRequestHandler<InsertBlackListCommand, Response<BlackListDto>>
        {
            private readonly ILogger<InsertBlackListHandler> _logger;
            private readonly IBlackListRepository _repository;
            public InsertBlackListHandler(ILogger<InsertBlackListHandler> logger, IBlackListRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<BlackListDto>> Handle(InsertBlackListCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}