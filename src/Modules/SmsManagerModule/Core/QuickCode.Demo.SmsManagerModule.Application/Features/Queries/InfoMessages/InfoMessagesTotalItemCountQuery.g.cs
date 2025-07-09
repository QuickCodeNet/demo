using System.Linq;
using QuickCode.Demo.Common.Mediator;
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
    public class InfoMessagesTotalItemCountQuery : IRequest<Response<int>>
    {
        public InfoMessagesTotalItemCountQuery()
        {
        }

        public class InfoMessagesTotalItemCountHandler : IRequestHandler<InfoMessagesTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<InfoMessagesTotalItemCountHandler> _logger;
            private readonly IInfoMessagesRepository _repository;
            public InfoMessagesTotalItemCountHandler(ILogger<InfoMessagesTotalItemCountHandler> logger, IInfoMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(InfoMessagesTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}