using AutoMapper;
using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos;

namespace QuickCode.Demo.EmailManagerModule.Application.Features
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
            private readonly IMapper _mapper;
            private readonly ICampaignMessagesRepository _repository;
            public CampaignMessagesGetItemHandler(IMapper mapper, ILogger<CampaignMessagesGetItemHandler> logger, ICampaignMessagesRepository repository)
            {
                _mapper = mapper;
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CampaignMessagesDto>> Handle(CampaignMessagesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = _mapper.Map<Response<CampaignMessagesDto>>(await _repository.GetByPkAsync(request.Id));
                return returnValue;
            }
        }
    }
}