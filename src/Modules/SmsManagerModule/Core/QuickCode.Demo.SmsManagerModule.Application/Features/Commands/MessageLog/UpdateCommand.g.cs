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
    public class UpdateMessageLogCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public MessageLogDto request { get; set; }

        public UpdateMessageLogCommand(int id, MessageLogDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class UpdateMessageLogHandler : IRequestHandler<UpdateMessageLogCommand, Response<bool>>
        {
            private readonly ILogger<UpdateMessageLogHandler> _logger;
            private readonly IMessageLogRepository _repository;
            public UpdateMessageLogHandler(ILogger<UpdateMessageLogHandler> logger, IMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateMessageLogCommand request, CancellationToken cancellationToken)
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