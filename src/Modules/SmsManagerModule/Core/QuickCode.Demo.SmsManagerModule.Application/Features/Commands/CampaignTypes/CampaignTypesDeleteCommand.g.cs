using AutoMapper;
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
    public class CampaignTypesDeleteCommand : IRequest<Response<bool>>
    {
        public CampaignTypesDto request { get; set; }

        public CampaignTypesDeleteCommand(CampaignTypesDto request)
        {
            this.request = request;
        }

        public class CampaignTypesDeleteHandler : IRequestHandler<CampaignTypesDeleteCommand, Response<bool>>
        {
            private readonly ILogger<CampaignTypesDeleteHandler> _logger;
            private readonly IMapper _mapper;
            private readonly ICampaignTypesRepository _repository;
            public CampaignTypesDeleteHandler(IMapper mapper, ILogger<CampaignTypesDeleteHandler> logger, ICampaignTypesRepository repository)
            {
                _mapper = mapper;
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(CampaignTypesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = _mapper.Map<CampaignTypes>(request.request);
                var returnValue = _mapper.Map<Response<bool>>(await _repository.DeleteAsync(model));
                return returnValue;
            }
        }
    }
}