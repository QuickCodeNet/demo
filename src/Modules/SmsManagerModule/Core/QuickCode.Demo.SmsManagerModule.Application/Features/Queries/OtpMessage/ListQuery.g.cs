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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessage;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessage
{
    public class ListOtpMessageQuery : IRequest<Response<List<OtpMessageDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ListOtpMessageQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ListOtpMessageHandler : IRequestHandler<ListOtpMessageQuery, Response<List<OtpMessageDto>>>
        {
            private readonly ILogger<ListOtpMessageHandler> _logger;
            private readonly IOtpMessageRepository _repository;
            public ListOtpMessageHandler(ILogger<ListOtpMessageHandler> logger, IOtpMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<OtpMessageDto>>> Handle(ListOtpMessageQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}