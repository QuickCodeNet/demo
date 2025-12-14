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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.OtpMessageQueue;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.OtpMessageQueue
{
    public class UpdateOtpMessageQueueCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public OtpMessageQueueDto request { get; set; }

        public UpdateOtpMessageQueueCommand(int id, OtpMessageQueueDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class UpdateOtpMessageQueueHandler : IRequestHandler<UpdateOtpMessageQueueCommand, Response<bool>>
        {
            private readonly ILogger<UpdateOtpMessageQueueHandler> _logger;
            private readonly IOtpMessageQueueRepository _repository;
            public UpdateOtpMessageQueueHandler(ILogger<UpdateOtpMessageQueueHandler> logger, IOtpMessageQueueRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateOtpMessageQueueCommand request, CancellationToken cancellationToken)
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