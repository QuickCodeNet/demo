using System;
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
    public class InfoTypesGetInfoMessagesForInfoTypesQuery : IRequest<Response<List<InfoTypesGetInfoMessagesForInfoTypesResponseDto>>>
    {
        public int InfoTypesId { get; set; }

        public InfoTypesGetInfoMessagesForInfoTypesQuery(int infoTypesId)
        {
            this.InfoTypesId = infoTypesId;
        }

        public class InfoTypesGetInfoMessagesForInfoTypesHandler : IRequestHandler<InfoTypesGetInfoMessagesForInfoTypesQuery, Response<List<InfoTypesGetInfoMessagesForInfoTypesResponseDto>>>
        {
            private readonly ILogger<InfoTypesGetInfoMessagesForInfoTypesHandler> _logger;
            private readonly IInfoTypesRepository _repository;
            public InfoTypesGetInfoMessagesForInfoTypesHandler(ILogger<InfoTypesGetInfoMessagesForInfoTypesHandler> logger, IInfoTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<InfoTypesGetInfoMessagesForInfoTypesResponseDto>>> Handle(InfoTypesGetInfoMessagesForInfoTypesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.InfoTypesGetInfoMessagesForInfoTypesAsync(request.InfoTypesId);
                return returnValue.ToResponse();
            }
        }
    }
}