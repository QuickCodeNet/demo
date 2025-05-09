using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SmsManagerModule.Application.Dtos;

namespace QuickCode.Demo.SmsManagerModule.Application.Features
{
    public class OtpMessagesTotalItemCountQuery : IRequest<Response<int>>
    {
        public OtpMessagesTotalItemCountQuery()
        {
        }

        public class OtpMessagesTotalItemCountHandler : IRequestHandler<OtpMessagesTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<OtpMessagesTotalItemCountHandler> _logger;
            private readonly IOtpMessagesRepository _repository;
            public OtpMessagesTotalItemCountHandler(ILogger<OtpMessagesTotalItemCountHandler> logger, IOtpMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(OtpMessagesTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}