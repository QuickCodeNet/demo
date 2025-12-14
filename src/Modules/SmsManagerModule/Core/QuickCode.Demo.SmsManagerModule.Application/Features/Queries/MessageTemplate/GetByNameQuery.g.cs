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
    public class GetByNameQuery : IRequest<Response<GetByNameResponseDto>>
    {
        public string MessageTemplatesName { get; set; }

        public GetByNameQuery(string messageTemplatesName)
        {
            this.MessageTemplatesName = messageTemplatesName;
        }

        public class GetByNameHandler : IRequestHandler<GetByNameQuery, Response<GetByNameResponseDto>>
        {
            private readonly ILogger<GetByNameHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public GetByNameHandler(ILogger<GetByNameHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetByNameResponseDto>> Handle(GetByNameQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByNameAsync(request.MessageTemplatesName);
                return returnValue.ToResponse();
            }
        }
    }
}