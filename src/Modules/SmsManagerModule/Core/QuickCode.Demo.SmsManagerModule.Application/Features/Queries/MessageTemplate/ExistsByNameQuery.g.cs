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
    public class ExistsByNameQuery : IRequest<Response<bool>>
    {
        public string MessageTemplatesName { get; set; }

        public ExistsByNameQuery(string messageTemplatesName)
        {
            this.MessageTemplatesName = messageTemplatesName;
        }

        public class ExistsByNameHandler : IRequestHandler<ExistsByNameQuery, Response<bool>>
        {
            private readonly ILogger<ExistsByNameHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public ExistsByNameHandler(ILogger<ExistsByNameHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ExistsByNameQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExistsByNameAsync(request.MessageTemplatesName);
                return returnValue.ToResponse();
            }
        }
    }
}