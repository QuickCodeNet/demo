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
    public class AddToBlacklistCommand : IRequest<Response<int>>
    {
        public string BlackListsRecipient { get; set; }
        public AddToBlacklistRequestDto UpdateRequest { get; set; }

        public AddToBlacklistCommand(string blackListsRecipient, AddToBlacklistRequestDto updateRequest)
        {
            this.BlackListsRecipient = blackListsRecipient;
            this.UpdateRequest = updateRequest;
        }

        public class AddToBlacklistHandler : IRequestHandler<AddToBlacklistCommand, Response<int>>
        {
            private readonly ILogger<AddToBlacklistHandler> _logger;
            private readonly IBlackListRepository _repository;
            public AddToBlacklistHandler(ILogger<AddToBlacklistHandler> logger, IBlackListRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(AddToBlacklistCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AddToBlacklistAsync(request.BlackListsRecipient, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}