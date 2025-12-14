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
using QuickCode.Demo.SmsManagerModule.Application.Dtos.BlackList;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.BlackList
{
    public class ExistsByRecipientQuery : IRequest<Response<bool>>
    {
        public string BlackListsRecipient { get; set; }

        public ExistsByRecipientQuery(string blackListsRecipient)
        {
            this.BlackListsRecipient = blackListsRecipient;
        }

        public class ExistsByRecipientHandler : IRequestHandler<ExistsByRecipientQuery, Response<bool>>
        {
            private readonly ILogger<ExistsByRecipientHandler> _logger;
            private readonly IBlackListRepository _repository;
            public ExistsByRecipientHandler(ILogger<ExistsByRecipientHandler> logger, IBlackListRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ExistsByRecipientQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ExistsByRecipientAsync(request.BlackListsRecipient);
                return returnValue.ToResponse();
            }
        }
    }
}