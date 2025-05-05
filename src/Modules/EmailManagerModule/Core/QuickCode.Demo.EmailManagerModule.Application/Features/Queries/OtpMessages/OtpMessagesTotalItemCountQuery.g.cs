using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos;

namespace QuickCode.Demo.EmailManagerModule.Application.Features
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