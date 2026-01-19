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
    public class DeleteMessageTemplateCommand : IRequest<Response<bool>>
    {
        public MessageTemplateDto request { get; set; }

        public DeleteMessageTemplateCommand(MessageTemplateDto request)
        {
            this.request = request;
        }

        public class DeleteMessageTemplateHandler : IRequestHandler<DeleteMessageTemplateCommand, Response<bool>>
        {
            private readonly ILogger<DeleteMessageTemplateHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public DeleteMessageTemplateHandler(ILogger<DeleteMessageTemplateHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteMessageTemplateCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}