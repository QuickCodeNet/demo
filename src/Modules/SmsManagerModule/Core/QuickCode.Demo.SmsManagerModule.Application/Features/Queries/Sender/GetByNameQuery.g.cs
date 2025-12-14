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
    public class GetByNameQuery : IRequest<Response<GetByNameResponseDto>>
    {
        public string SendersName { get; set; }

        public GetByNameQuery(string sendersName)
        {
            this.SendersName = sendersName;
        }

        public class GetByNameHandler : IRequestHandler<GetByNameQuery, Response<GetByNameResponseDto>>
        {
            private readonly ILogger<GetByNameHandler> _logger;
            private readonly ISenderRepository _repository;
            public GetByNameHandler(ILogger<GetByNameHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetByNameResponseDto>> Handle(GetByNameQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByNameAsync(request.SendersName);
                return returnValue.ToResponse();
            }
        }
    }
}