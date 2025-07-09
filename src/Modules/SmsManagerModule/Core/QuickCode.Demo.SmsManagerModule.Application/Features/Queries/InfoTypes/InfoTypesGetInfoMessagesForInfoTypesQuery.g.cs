using System;
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