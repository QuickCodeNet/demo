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
    public class UpdateSenderCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public SenderDto request { get; set; }

        public UpdateSenderCommand(int id, SenderDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class UpdateSenderHandler : IRequestHandler<UpdateSenderCommand, Response<bool>>
        {
            private readonly ILogger<UpdateSenderHandler> _logger;
            private readonly ISenderRepository _repository;
            public UpdateSenderHandler(ILogger<UpdateSenderHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateSenderCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}