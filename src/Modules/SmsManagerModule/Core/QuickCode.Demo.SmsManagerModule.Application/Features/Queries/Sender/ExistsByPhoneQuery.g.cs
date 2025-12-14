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
    public class ExistsByPhoneQuery : IRequest<Response<bool>>
    {
        public string SendersPhoneNumber { get; set; }

        public ExistsByPhoneQuery(string sendersPhoneNumber)
        {
            this.SendersPhoneNumber = sendersPhoneNumber;
        }

        public class ExistsByPhoneHandler : IRequestHandler<ExistsByPhoneQuery, Response<bool>>
        {
            private readonly ILogger<ExistsByPhoneHandler> _logger;
            private readonly ISenderRepository _repository;
            public ExistsByPhoneHandler(ILogger<ExistsByPhoneHandler> logger, ISenderRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ExistsByPhoneQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExistsByPhoneAsync(request.SendersPhoneNumber);
                return returnValue.ToResponse();
            }
        }
    }
}