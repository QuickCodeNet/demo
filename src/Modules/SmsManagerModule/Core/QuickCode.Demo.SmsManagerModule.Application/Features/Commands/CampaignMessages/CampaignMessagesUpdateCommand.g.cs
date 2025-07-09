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
    public class CampaignMessagesUpdateCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public CampaignMessagesDto request { get; set; }

        public CampaignMessagesUpdateCommand(int id, CampaignMessagesDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class CampaignMessagesUpdateHandler : IRequestHandler<CampaignMessagesUpdateCommand, Response<bool>>
        {
            private readonly ILogger<CampaignMessagesUpdateHandler> _logger;
            private readonly ICampaignMessagesRepository _repository;
            public CampaignMessagesUpdateHandler(ILogger<CampaignMessagesUpdateHandler> logger, ICampaignMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(CampaignMessagesUpdateCommand request, CancellationToken cancellationToken)
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