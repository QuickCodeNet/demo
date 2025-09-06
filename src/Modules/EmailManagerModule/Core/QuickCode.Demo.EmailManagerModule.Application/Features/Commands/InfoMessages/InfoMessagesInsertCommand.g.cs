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