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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageTemplate;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.MessageTemplate
{
    public class TotalCountMessageTemplateQuery : IRequest<Response<int>>
    {
        public TotalCountMessageTemplateQuery()
        {
        }

        public class TotalCountMessageTemplateHandler : IRequestHandler<TotalCountMessageTemplateQuery, Response<int>>
        {
            private readonly ILogger<TotalCountMessageTemplateHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public TotalCountMessageTemplateHandler(ILogger<TotalCountMessageTemplateHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountMessageTemplateQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}