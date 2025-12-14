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
    public class GetItemMessageTemplateQuery : IRequest<Response<MessageTemplateDto>>
    {
        public string Name { get; set; }

        public GetItemMessageTemplateQuery(string name)
        {
            this.Name = name;
        }

        public class GetItemMessageTemplateHandler : IRequestHandler<GetItemMessageTemplateQuery, Response<MessageTemplateDto>>
        {
            private readonly ILogger<GetItemMessageTemplateHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public GetItemMessageTemplateHandler(ILogger<GetItemMessageTemplateHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<MessageTemplateDto>> Handle(GetItemMessageTemplateQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Name);
                return returnValue.ToResponse();
            }
        }
    }
}