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
    public class ListOtpMessageLogQuery : IRequest<Response<List<OtpMessageLogDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ListOtpMessageLogQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ListOtpMessageLogHandler : IRequestHandler<ListOtpMessageLogQuery, Response<List<OtpMessageLogDto>>>
        {
            private readonly ILogger<ListOtpMessageLogHandler> _logger;
            private readonly IOtpMessageLogRepository _repository;
            public ListOtpMessageLogHandler(ILogger<ListOtpMessageLogHandler> logger, IOtpMessageLogRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<OtpMessageLogDto>>> Handle(ListOtpMessageLogQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}