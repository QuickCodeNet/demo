using System;
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
    public class InfoTypesInfoTypesInfoMessagesRestQuery : IRequest<Response<List<InfoTypesInfoMessagesRestResponseDto>>>
    {
        public int InfoTypesId { get; set; }

        public InfoTypesInfoTypesInfoMessagesRestQuery(int infoTypesId)
        {
            this.InfoTypesId = infoTypesId;
        }

        public class InfoTypesInfoTypesInfoMessagesRestHandler : IRequestHandler<InfoTypesInfoTypesInfoMessagesRestQuery, Response<List<InfoTypesInfoMessagesRestResponseDto>>>
        {
            private readonly ILogger<InfoTypesInfoTypesInfoMessagesRestHandler> _logger;
            private readonly IInfoTypesRepository _repository;
            public InfoTypesInfoTypesInfoMessagesRestHandler(ILogger<InfoTypesInfoTypesInfoMessagesRestHandler> logger, IInfoTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<InfoTypesInfoMessagesRestResponseDto>>> Handle(InfoTypesInfoTypesInfoMessagesRestQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.InfoTypesInfoMessagesRestAsync(request.InfoTypesId);
                return returnValue.ToResponse();
            }
        }
    }
}