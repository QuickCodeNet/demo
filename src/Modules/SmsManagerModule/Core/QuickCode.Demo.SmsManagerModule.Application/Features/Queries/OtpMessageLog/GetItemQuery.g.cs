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
    public class GetItemOtpMessageLogQuery : IRequest<Response<OtpMessageLogDto>>
    {
        public int Id { get; set; }

        public GetItemOtpMessageLogQuery(int id)
        {
            this.Id = id;
        }

        public class GetItemOtpMessageLogHandler : IRequestHandler<GetItemOtpMessageLogQuery, Response<OtpMessageLogDto>>
        {
            private readonly ILogger<GetItemOtpMessageLogHandler> _logger;
            private readonly IOtpMessageLogRepository _repository;
            public GetItemOtpMessageLogHandler(ILogger<GetItemOtpMessageLogHandler> logger, IOtpMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<OtpMessageLogDto>> Handle(GetItemOtpMessageLogQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}