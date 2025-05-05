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
    public class CampaignMessagesDeleteCommand : IRequest<Response<bool>>
    {
        public CampaignMessagesDto request { get; set; }

        public CampaignMessagesDeleteCommand(CampaignMessagesDto request)
        {
            this.request = request;
        }

        public class CampaignMessagesDeleteHandler : IRequestHandler<CampaignMessagesDeleteCommand, Response<bool>>
        {
            private readonly ILogger<CampaignMessagesDeleteHandler> _logger;
            private readonly ICampaignMessagesRepository _repository;
            public CampaignMessagesDeleteHandler(ILogger<CampaignMessagesDeleteHandler> logger, ICampaignMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(CampaignMessagesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}