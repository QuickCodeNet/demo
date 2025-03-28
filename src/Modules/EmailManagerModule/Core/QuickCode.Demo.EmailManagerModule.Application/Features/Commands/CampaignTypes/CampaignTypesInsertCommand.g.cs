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
    public class CampaignTypesInsertCommand : IRequest<Response<CampaignTypesDto>>
    {
        public CampaignTypesDto request { get; set; }

        public CampaignTypesInsertCommand(CampaignTypesDto request)
        {
            this.request = request;
        }

        public class CampaignTypesInsertHandler : IRequestHandler<CampaignTypesInsertCommand, Response<CampaignTypesDto>>
        {
            private readonly ILogger<CampaignTypesInsertHandler> _logger;
            private readonly IMapper _mapper;
            private readonly ICampaignTypesRepository _repository;
            public CampaignTypesInsertHandler(IMapper mapper, ILogger<CampaignTypesInsertHandler> logger, ICampaignTypesRepository repository)
            {
                _mapper = mapper;
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CampaignTypesDto>> Handle(CampaignTypesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = _mapper.Map<CampaignTypes>(request.request);
                var returnValue = _mapper.Map<Response<CampaignTypesDto>>(await _repository.InsertAsync(model));
                return returnValue;
            }
        }
    }
}