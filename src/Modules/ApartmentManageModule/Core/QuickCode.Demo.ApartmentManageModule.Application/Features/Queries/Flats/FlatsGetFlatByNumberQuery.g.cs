using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Features
{
    public class FlatsGetFlatByNumberQuery : IRequest<Response<FlatsGetFlatByNumberResponseDto>>
    {
        public int FlatsSiteId { get; set; }
        public string FlatsFlatNumber { get; set; }

        public FlatsGetFlatByNumberQuery(int flatsSiteId, string flatsFlatNumber)
        {
            this.FlatsSiteId = flatsSiteId;
            this.FlatsFlatNumber = flatsFlatNumber;
        }

        public class FlatsGetFlatByNumberHandler : IRequestHandler<FlatsGetFlatByNumberQuery, Response<FlatsGetFlatByNumberResponseDto>>
        {
            private readonly ILogger<FlatsGetFlatByNumberHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetFlatByNumberHandler(ILogger<FlatsGetFlatByNumberHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FlatsGetFlatByNumberResponseDto>> Handle(FlatsGetFlatByNumberQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetFlatByNumberAsync(request.FlatsSiteId, request.FlatsFlatNumber);
                return returnValue.ToResponse();
            }
        }
    }
}