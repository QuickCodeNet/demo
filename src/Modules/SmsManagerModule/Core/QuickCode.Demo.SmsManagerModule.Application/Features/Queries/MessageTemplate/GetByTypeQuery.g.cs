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
    public class GetByTypeQuery : IRequest<Response<List<GetByTypeResponseDto>>>
    {
        public TemplateTypes MessageTemplatesType { get; set; }

        public GetByTypeQuery(TemplateTypes messageTemplatesType)
        {
            this.MessageTemplatesType = messageTemplatesType;
        }

        public class GetByTypeHandler : IRequestHandler<GetByTypeQuery, Response<List<GetByTypeResponseDto>>>
        {
            private readonly ILogger<GetByTypeHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public GetByTypeHandler(ILogger<GetByTypeHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetByTypeResponseDto>>> Handle(GetByTypeQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByTypeAsync(request.MessageTemplatesType);
                return returnValue.ToResponse();
            }
        }
    }
}