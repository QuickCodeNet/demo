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
    public class CampaignMessagesGetItemQuery : IRequest<Response<CampaignMessagesDto>>
    {
        public int Id { get; set; }

        public CampaignMessagesGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class CampaignMessagesGetItemHandler : IRequestHandler<CampaignMessagesGetItemQuery, Response<CampaignMessagesDto>>
        {
            private readonly ILogger<CampaignMessagesGetItemHandler> _logger;
            private readonly ICampaignMessagesRepository _repository;
            public CampaignMessagesGetItemHandler(ILogger<CampaignMessagesGetItemHandler> logger, ICampaignMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CampaignMessagesDto>> Handle(CampaignMessagesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}