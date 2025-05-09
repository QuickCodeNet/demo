using System.Linq;
using MediatR;
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
    public class CampaignMessagesListQuery : IRequest<Response<List<CampaignMessagesDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public CampaignMessagesListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class CampaignMessagesListHandler : IRequestHandler<CampaignMessagesListQuery, Response<List<CampaignMessagesDto>>>
        {
            private readonly ILogger<CampaignMessagesListHandler> _logger;
            private readonly ICampaignMessagesRepository _repository;
            public CampaignMessagesListHandler(ILogger<CampaignMessagesListHandler> logger, ICampaignMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<CampaignMessagesDto>>> Handle(CampaignMessagesListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}