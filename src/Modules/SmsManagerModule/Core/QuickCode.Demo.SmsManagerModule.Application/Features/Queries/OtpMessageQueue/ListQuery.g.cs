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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessageQueue;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessageQueue
{
    public class ListOtpMessageQueueQuery : IRequest<Response<List<OtpMessageQueueDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ListOtpMessageQueueQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ListOtpMessageQueueHandler : IRequestHandler<ListOtpMessageQueueQuery, Response<List<OtpMessageQueueDto>>>
        {
            private readonly ILogger<ListOtpMessageQueueHandler> _logger;
            private readonly IOtpMessageQueueRepository _repository;
            public ListOtpMessageQueueHandler(ILogger<ListOtpMessageQueueHandler> logger, IOtpMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<OtpMessageQueueDto>>> Handle(ListOtpMessageQueueQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}