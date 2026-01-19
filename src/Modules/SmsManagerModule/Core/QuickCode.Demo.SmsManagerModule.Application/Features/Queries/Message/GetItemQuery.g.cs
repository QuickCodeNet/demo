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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.Message;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.Message
{
    public class GetItemMessageQuery : IRequest<Response<MessageDto>>
    {
        public int Id { get; set; }

        public GetItemMessageQuery(int id)
        {
            this.Id = id;
        }

        public class GetItemMessageHandler : IRequestHandler<GetItemMessageQuery, Response<MessageDto>>
        {
            private readonly ILogger<GetItemMessageHandler> _logger;
            private readonly IMessageRepository _repository;
            public GetItemMessageHandler(ILogger<GetItemMessageHandler> logger, IMessageRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<MessageDto>> Handle(GetItemMessageQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}