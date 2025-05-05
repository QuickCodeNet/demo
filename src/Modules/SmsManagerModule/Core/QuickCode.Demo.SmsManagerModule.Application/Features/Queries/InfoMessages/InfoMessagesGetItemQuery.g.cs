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
    public class InfoMessagesGetItemQuery : IRequest<Response<InfoMessagesDto>>
    {
        public int Id { get; set; }

        public InfoMessagesGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class InfoMessagesGetItemHandler : IRequestHandler<InfoMessagesGetItemQuery, Response<InfoMessagesDto>>
        {
            private readonly ILogger<InfoMessagesGetItemHandler> _logger;
            private readonly IInfoMessagesRepository _repository;
            public InfoMessagesGetItemHandler(ILogger<InfoMessagesGetItemHandler> logger, IInfoMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<InfoMessagesDto>> Handle(InfoMessagesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}