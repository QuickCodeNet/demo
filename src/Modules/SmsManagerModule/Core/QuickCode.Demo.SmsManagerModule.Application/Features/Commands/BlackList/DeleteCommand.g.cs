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
    public class DeleteBlackListCommand : IRequest<Response<bool>>
    {
        public BlackListDto request { get; set; }

        public DeleteBlackListCommand(BlackListDto request)
        {
            this.request = request;
        }

        public class DeleteBlackListHandler : IRequestHandler<DeleteBlackListCommand, Response<bool>>
        {
            private readonly ILogger<DeleteBlackListHandler> _logger;
            private readonly IBlackListRepository _repository;
            public DeleteBlackListHandler(ILogger<DeleteBlackListHandler> logger, IBlackListRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteBlackListCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}