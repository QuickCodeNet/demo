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
    public class UpdateBlackListCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public BlackListDto request { get; set; }

        public UpdateBlackListCommand(int id, BlackListDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class UpdateBlackListHandler : IRequestHandler<UpdateBlackListCommand, Response<bool>>
        {
            private readonly ILogger<UpdateBlackListHandler> _logger;
            private readonly IBlackListRepository _repository;
            public UpdateBlackListHandler(ILogger<UpdateBlackListHandler> logger, IBlackListRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateBlackListCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}