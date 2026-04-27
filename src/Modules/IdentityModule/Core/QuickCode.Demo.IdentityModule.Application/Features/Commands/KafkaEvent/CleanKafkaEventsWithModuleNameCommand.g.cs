using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.IdentityModule.Domain.Entities;
using QuickCode.Demo.IdentityModule.Application.Interfaces.Repositories;
using QuickCode.Demo.IdentityModule.Application.Dtos.KafkaEvent;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.KafkaEvent
{
    public class CleanKafkaEventsWithModuleNameCommand : IRequest<Response<int>>
    {
        public string ApiMethodDefinitionsModuleName { get; set; }

        public CleanKafkaEventsWithModuleNameCommand(string apiMethodDefinitionsModuleName)
        {
            this.ApiMethodDefinitionsModuleName = apiMethodDefinitionsModuleName;
        }

        public class CleanKafkaEventsWithModuleNameHandler : IRequestHandler<CleanKafkaEventsWithModuleNameCommand, Response<int>>
        {
            private readonly ILogger<CleanKafkaEventsWithModuleNameHandler> _logger;
            private readonly IKafkaEventRepository _repository;
            public CleanKafkaEventsWithModuleNameHandler(ILogger<CleanKafkaEventsWithModuleNameHandler> logger, IKafkaEventRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(CleanKafkaEventsWithModuleNameCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CleanKafkaEventsWithModuleNameAsync(request.ApiMethodDefinitionsModuleName);
                return returnValue.ToResponse();
            }
        }
    }
}