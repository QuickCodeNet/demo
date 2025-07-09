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
    public class InfoTypesGetInfoMessagesForInfoTypesDetailsQuery : IRequest<Response<InfoTypesGetInfoMessagesForInfoTypesResponseDto>>
    {
        public int InfoTypesId { get; set; }
        public int InfoMessagesId { get; set; }

        public InfoTypesGetInfoMessagesForInfoTypesDetailsQuery(int infoTypesId, int infoMessagesId)
        {
            this.InfoTypesId = infoTypesId;
            this.InfoMessagesId = infoMessagesId;
        }

        public class InfoTypesGetInfoMessagesForInfoTypesDetailsHandler : IRequestHandler<InfoTypesGetInfoMessagesForInfoTypesDetailsQuery, Response<InfoTypesGetInfoMessagesForInfoTypesResponseDto>>
        {
            private readonly ILogger<InfoTypesGetInfoMessagesForInfoTypesDetailsHandler> _logger;
            private readonly IInfoTypesRepository _repository;
            public InfoTypesGetInfoMessagesForInfoTypesDetailsHandler(ILogger<InfoTypesGetInfoMessagesForInfoTypesDetailsHandler> logger, IInfoTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<InfoTypesGetInfoMessagesForInfoTypesResponseDto>> Handle(InfoTypesGetInfoMessagesForInfoTypesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.InfoTypesGetInfoMessagesForInfoTypesDetailsAsync(request.InfoTypesId, request.InfoMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}