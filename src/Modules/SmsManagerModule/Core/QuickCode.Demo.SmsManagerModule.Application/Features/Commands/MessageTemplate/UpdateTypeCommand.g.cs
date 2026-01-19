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
    public class UpdateTypeCommand : IRequest<Response<int>>
    {
        public string MessageTemplatesName { get; set; }
        public UpdateTypeRequestDto UpdateRequest { get; set; }

        public UpdateTypeCommand(string messageTemplatesName, UpdateTypeRequestDto updateRequest)
        {
            this.MessageTemplatesName = messageTemplatesName;
            this.UpdateRequest = updateRequest;
        }

        public class UpdateTypeHandler : IRequestHandler<UpdateTypeCommand, Response<int>>
        {
            private readonly ILogger<UpdateTypeHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public UpdateTypeHandler(ILogger<UpdateTypeHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.UpdateTypeAsync(request.MessageTemplatesName, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}