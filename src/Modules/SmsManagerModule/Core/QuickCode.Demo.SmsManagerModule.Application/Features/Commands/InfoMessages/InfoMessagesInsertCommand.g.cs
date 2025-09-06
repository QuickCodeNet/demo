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
    public class InfoMessagesInsertCommand : IRequest<Response<InfoMessagesDto>>
    {
        public InfoMessagesDto request { get; set; }

        public InfoMessagesInsertCommand(InfoMessagesDto request)
        {
            this.request = request;
        }

        public class InfoMessagesInsertHandler : IRequestHandler<InfoMessagesInsertCommand, Response<InfoMessagesDto>>
        {
            private readonly ILogger<InfoMessagesInsertHandler> _logger;
            private readonly IInfoMessagesRepository _repository;
            public InfoMessagesInsertHandler(ILogger<InfoMessagesInsertHandler> logger, IInfoMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<InfoMessagesDto>> Handle(InfoMessagesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}