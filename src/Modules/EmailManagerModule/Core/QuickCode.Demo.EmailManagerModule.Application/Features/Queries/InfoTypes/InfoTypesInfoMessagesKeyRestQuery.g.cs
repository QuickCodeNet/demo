using System;
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
    public class InfoTypesInfoTypesInfoMessagesKeyRestQuery : IRequest<Response<InfoTypesInfoMessagesKeyRestResponseDto>>
    {
        public int InfoTypesId { get; set; }
        public int InfoMessagesId { get; set; }

        public InfoTypesInfoTypesInfoMessagesKeyRestQuery(int infoTypesId, int infoMessagesId)
        {
            this.InfoTypesId = infoTypesId;
            this.InfoMessagesId = infoMessagesId;
        }

        public class InfoTypesInfoTypesInfoMessagesKeyRestHandler : IRequestHandler<InfoTypesInfoTypesInfoMessagesKeyRestQuery, Response<InfoTypesInfoMessagesKeyRestResponseDto>>
        {
            private readonly ILogger<InfoTypesInfoTypesInfoMessagesKeyRestHandler> _logger;
            private readonly IInfoTypesRepository _repository;
            public InfoTypesInfoTypesInfoMessagesKeyRestHandler(ILogger<InfoTypesInfoTypesInfoMessagesKeyRestHandler> logger, IInfoTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<InfoTypesInfoMessagesKeyRestResponseDto>> Handle(InfoTypesInfoTypesInfoMessagesKeyRestQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.InfoTypesInfoMessagesKeyRestAsync(request.InfoTypesId, request.InfoMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}