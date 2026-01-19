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
    public class UpdateContentCommand : IRequest<Response<int>>
    {
        public string MessageTemplatesName { get; set; }
        public UpdateContentRequestDto UpdateRequest { get; set; }

        public UpdateContentCommand(string messageTemplatesName, UpdateContentRequestDto updateRequest)
        {
            this.MessageTemplatesName = messageTemplatesName;
            this.UpdateRequest = updateRequest;
        }

        public class UpdateContentHandler : IRequestHandler<UpdateContentCommand, Response<int>>
        {
            private readonly ILogger<UpdateContentHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public UpdateContentHandler(ILogger<UpdateContentHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.UpdateContentAsync(request.MessageTemplatesName, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}