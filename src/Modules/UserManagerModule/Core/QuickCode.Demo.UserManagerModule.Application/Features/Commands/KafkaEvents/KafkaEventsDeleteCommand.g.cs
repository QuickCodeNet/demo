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
    public class KafkaEventsDeleteCommand : IRequest<Response<bool>>
    {
        public KafkaEventsDto request { get; set; }

        public KafkaEventsDeleteCommand(KafkaEventsDto request)
        {
            this.request = request;
        }

        public class KafkaEventsDeleteHandler : IRequestHandler<KafkaEventsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<KafkaEventsDeleteHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsDeleteHandler(ILogger<KafkaEventsDeleteHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(KafkaEventsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}