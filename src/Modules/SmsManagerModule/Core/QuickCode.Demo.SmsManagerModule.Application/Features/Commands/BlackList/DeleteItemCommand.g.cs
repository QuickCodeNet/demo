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
    public class DeleteItemBlackListCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public DeleteItemBlackListCommand(int id)
        {
            this.Id = id;
        }

        public class DeleteItemBlackListHandler : IRequestHandler<DeleteItemBlackListCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemBlackListHandler> _logger;
            private readonly IBlackListRepository _repository;
            public DeleteItemBlackListHandler(ILogger<DeleteItemBlackListHandler> logger, IBlackListRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemBlackListCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Id);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}