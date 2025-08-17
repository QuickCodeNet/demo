using System.Linq;
using QuickCode.Demo.Common.Mediator;
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
            private readonly ICampaignTypesRepository _repository;
            public CampaignTypesDeleteHandler(ILogger<CampaignTypesDeleteHandler> logger, ICampaignTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(CampaignTypesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}