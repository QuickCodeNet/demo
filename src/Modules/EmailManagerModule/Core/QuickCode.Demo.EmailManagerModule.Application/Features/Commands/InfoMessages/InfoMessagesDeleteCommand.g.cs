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