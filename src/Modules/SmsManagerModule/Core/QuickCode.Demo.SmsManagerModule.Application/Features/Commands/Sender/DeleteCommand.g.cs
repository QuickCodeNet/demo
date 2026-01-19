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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.Sender;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.Sender
{
    public class DeleteSenderCommand : IRequest<Response<bool>>
    {
        public SenderDto request { get; set; }

        public DeleteSenderCommand(SenderDto request)
        {
            this.request = request;
        }

        public class DeleteSenderHandler : IRequestHandler<DeleteSenderCommand, Response<bool>>
        {
            private readonly ILogger<DeleteSenderHandler> _logger;
            private readonly ISenderRepository _repository;
            public DeleteSenderHandler(ILogger<DeleteSenderHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteSenderCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}