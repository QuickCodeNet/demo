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
    public class KafkaEventsListQuery : IRequest<Response<List<KafkaEventsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public KafkaEventsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class KafkaEventsListHandler : IRequestHandler<KafkaEventsListQuery, Response<List<KafkaEventsDto>>>
        {
            private readonly ILogger<KafkaEventsListHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsListHandler(ILogger<KafkaEventsListHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<KafkaEventsDto>>> Handle(KafkaEventsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}