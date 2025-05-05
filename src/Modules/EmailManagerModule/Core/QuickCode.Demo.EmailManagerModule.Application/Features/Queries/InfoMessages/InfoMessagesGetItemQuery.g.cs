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