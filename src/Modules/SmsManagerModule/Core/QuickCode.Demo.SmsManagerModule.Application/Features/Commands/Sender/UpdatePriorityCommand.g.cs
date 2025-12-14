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
    public class UpdatePriorityCommand : IRequest<Response<int>>
    {
        public int SendersId { get; set; }
        public UpdatePriorityRequestDto UpdateRequest { get; set; }

        public UpdatePriorityCommand(int sendersId, UpdatePriorityRequestDto updateRequest)
        {
            this.SendersId = sendersId;
            this.UpdateRequest = updateRequest;
        }

        public class UpdatePriorityHandler : IRequestHandler<UpdatePriorityCommand, Response<int>>
        {
            private readonly ILogger<UpdatePriorityHandler> _logger;
            private readonly ISenderRepository _repository;
            public UpdatePriorityHandler(ILogger<UpdatePriorityHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(UpdatePriorityCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.UpdatePriorityAsync(request.SendersId, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}