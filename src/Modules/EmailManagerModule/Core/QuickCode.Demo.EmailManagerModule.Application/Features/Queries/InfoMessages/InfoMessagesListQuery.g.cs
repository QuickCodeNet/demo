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
    public class InfoMessagesListQuery : IRequest<Response<List<InfoMessagesDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public InfoMessagesListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class InfoMessagesListHandler : IRequestHandler<InfoMessagesListQuery, Response<List<InfoMessagesDto>>>
        {
            private readonly ILogger<InfoMessagesListHandler> _logger;
            private readonly IInfoMessagesRepository _repository;
            public InfoMessagesListHandler(ILogger<InfoMessagesListHandler> logger, IInfoMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<InfoMessagesDto>>> Handle(InfoMessagesListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}