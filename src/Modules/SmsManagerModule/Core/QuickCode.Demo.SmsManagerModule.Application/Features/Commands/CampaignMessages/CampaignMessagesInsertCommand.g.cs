using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SmsManagerModule.Application.Dtos;

namespace QuickCode.Demo.SmsManagerModule.Application.Features
{
    public class CampaignMessagesInsertCommand : IRequest<Response<CampaignMessagesDto>>
    {
        public CampaignMessagesDto request { get; set; }

        public CampaignMessagesInsertCommand(CampaignMessagesDto request)
        {
            this.request = request;
        }

        public class CampaignMessagesInsertHandler : IRequestHandler<CampaignMessagesInsertCommand, Response<CampaignMessagesDto>>
        {
            private readonly ILogger<CampaignMessagesInsertHandler> _logger;
            private readonly ICampaignMessagesRepository _repository;
            public CampaignMessagesInsertHandler(ILogger<CampaignMessagesInsertHandler> logger, ICampaignMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CampaignMessagesDto>> Handle(CampaignMessagesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}