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
    public class GetItemSenderQuery : IRequest<Response<SenderDto>>
    {
        public int Id { get; set; }

        public GetItemSenderQuery(int id)
        {
            this.Id = id;
        }

        public class GetItemSenderHandler : IRequestHandler<GetItemSenderQuery, Response<SenderDto>>
        {
            private readonly ILogger<GetItemSenderHandler> _logger;
            private readonly ISenderRepository _repository;
            public GetItemSenderHandler(ILogger<GetItemSenderHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SenderDto>> Handle(GetItemSenderQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}