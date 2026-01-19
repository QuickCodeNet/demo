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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageLog;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.MessageLog
{
    public class DeleteItemMessageLogCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public DeleteItemMessageLogCommand(int id)
        {
            this.Id = id;
        }

        public class DeleteItemMessageLogHandler : IRequestHandler<DeleteItemMessageLogCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemMessageLogHandler> _logger;
            private readonly IMessageLogRepository _repository;
            public DeleteItemMessageLogHandler(ILogger<DeleteItemMessageLogHandler> logger, IMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemMessageLogCommand request, CancellationToken cancellationToken)
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