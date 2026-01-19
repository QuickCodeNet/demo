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
    public class UpdateMessageTemplateCommand : IRequest<Response<bool>>
    {
        public string Name { get; set; }
        public MessageTemplateDto request { get; set; }

        public UpdateMessageTemplateCommand(string name, MessageTemplateDto request)
        {
            this.request = request;
            this.Name = name;
        }

        public class UpdateMessageTemplateHandler : IRequestHandler<UpdateMessageTemplateCommand, Response<bool>>
        {
            private readonly ILogger<UpdateMessageTemplateHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public UpdateMessageTemplateHandler(ILogger<UpdateMessageTemplateHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateMessageTemplateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Name);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}