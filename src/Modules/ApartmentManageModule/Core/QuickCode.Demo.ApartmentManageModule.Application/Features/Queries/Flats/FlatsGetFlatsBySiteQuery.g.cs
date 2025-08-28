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
    public class FlatsGetFlatsBySiteQuery : IRequest<Response<List<FlatsGetFlatsBySiteResponseDto>>>
    {
        public int FlatsSiteId { get; set; }
        public bool FlatsIsActive { get; set; }

        public FlatsGetFlatsBySiteQuery(int flatsSiteId, bool flatsIsActive)
        {
            this.FlatsSiteId = flatsSiteId;
            this.FlatsIsActive = flatsIsActive;
        }

        public class FlatsGetFlatsBySiteHandler : IRequestHandler<FlatsGetFlatsBySiteQuery, Response<List<FlatsGetFlatsBySiteResponseDto>>>
        {
            private readonly ILogger<FlatsGetFlatsBySiteHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetFlatsBySiteHandler(ILogger<FlatsGetFlatsBySiteHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatsGetFlatsBySiteResponseDto>>> Handle(FlatsGetFlatsBySiteQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetFlatsBySiteAsync(request.FlatsSiteId, request.FlatsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}