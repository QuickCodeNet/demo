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
    public class CampaignTypesUpdateCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public CampaignTypesDto request { get; set; }

        public CampaignTypesUpdateCommand(int id, CampaignTypesDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class CampaignTypesUpdateHandler : IRequestHandler<CampaignTypesUpdateCommand, Response<bool>>
        {
            private readonly ILogger<CampaignTypesUpdateHandler> _logger;
            private readonly ICampaignTypesRepository _repository;
            public CampaignTypesUpdateHandler(ILogger<CampaignTypesUpdateHandler> logger, ICampaignTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(CampaignTypesUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}