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
    public class InfoMessagesDeleteCommand : IRequest<Response<bool>>
    {
        public InfoMessagesDto request { get; set; }

        public InfoMessagesDeleteCommand(InfoMessagesDto request)
        {
            this.request = request;
        }

        public class InfoMessagesDeleteHandler : IRequestHandler<InfoMessagesDeleteCommand, Response<bool>>
        {
            private readonly ILogger<InfoMessagesDeleteHandler> _logger;
            private readonly IInfoMessagesRepository _repository;
            public InfoMessagesDeleteHandler(ILogger<InfoMessagesDeleteHandler> logger, IInfoMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(InfoMessagesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}