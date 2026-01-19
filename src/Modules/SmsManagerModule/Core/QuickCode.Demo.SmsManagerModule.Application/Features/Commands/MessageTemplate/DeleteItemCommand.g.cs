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
    public class DeleteItemMessageTemplateCommand : IRequest<Response<bool>>
    {
        public string Name { get; set; }

        public DeleteItemMessageTemplateCommand(string name)
        {
            this.Name = name;
        }

        public class DeleteItemMessageTemplateHandler : IRequestHandler<DeleteItemMessageTemplateCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemMessageTemplateHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public DeleteItemMessageTemplateHandler(ILogger<DeleteItemMessageTemplateHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemMessageTemplateCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Name);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}