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
    public class InsertMessageTemplateCommand : IRequest<Response<MessageTemplateDto>>
    {
        public MessageTemplateDto request { get; set; }

        public InsertMessageTemplateCommand(MessageTemplateDto request)
        {
            this.request = request;
        }

        public class InsertMessageTemplateHandler : IRequestHandler<InsertMessageTemplateCommand, Response<MessageTemplateDto>>
        {
            private readonly ILogger<InsertMessageTemplateHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public InsertMessageTemplateHandler(ILogger<InsertMessageTemplateHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<MessageTemplateDto>> Handle(InsertMessageTemplateCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}