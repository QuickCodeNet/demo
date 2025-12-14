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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessageLog;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessageLog
{
    public class UpdateOtpMessageLogCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public OtpMessageLogDto request { get; set; }

        public UpdateOtpMessageLogCommand(int id, OtpMessageLogDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class UpdateOtpMessageLogHandler : IRequestHandler<UpdateOtpMessageLogCommand, Response<bool>>
        {
            private readonly ILogger<UpdateOtpMessageLogHandler> _logger;
            private readonly IOtpMessageLogRepository _repository;
            public UpdateOtpMessageLogHandler(ILogger<UpdateOtpMessageLogHandler> logger, IOtpMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateOtpMessageLogCommand request, CancellationToken cancellationToken)
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