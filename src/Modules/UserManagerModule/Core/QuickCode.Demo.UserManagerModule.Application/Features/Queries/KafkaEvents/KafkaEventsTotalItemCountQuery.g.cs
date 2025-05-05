using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class KafkaEventsTotalItemCountQuery : IRequest<Response<int>>
    {
        public KafkaEventsTotalItemCountQuery()
        {
        }

        public class KafkaEventsTotalItemCountHandler : IRequestHandler<KafkaEventsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<KafkaEventsTotalItemCountHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsTotalItemCountHandler(ILogger<KafkaEventsTotalItemCountHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(KafkaEventsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}