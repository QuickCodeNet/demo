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
    public class InsertSenderCommand : IRequest<Response<SenderDto>>
    {
        public SenderDto request { get; set; }

        public InsertSenderCommand(SenderDto request)
        {
            this.request = request;
        }

        public class InsertSenderHandler : IRequestHandler<InsertSenderCommand, Response<SenderDto>>
        {
            private readonly ILogger<InsertSenderHandler> _logger;
            private readonly ISenderRepository _repository;
            public InsertSenderHandler(ILogger<InsertSenderHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SenderDto>> Handle(InsertSenderCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}