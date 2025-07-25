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
    public class FlatsGetFlatsCountBySiteQuery : IRequest<Response<long>>
    {
        public int FlatsSiteId { get; set; }
        public bool FlatsIsActive { get; set; }

        public FlatsGetFlatsCountBySiteQuery(int flatsSiteId, bool flatsIsActive)
        {
            this.FlatsSiteId = flatsSiteId;
            this.FlatsIsActive = flatsIsActive;
        }

        public class FlatsGetFlatsCountBySiteHandler : IRequestHandler<FlatsGetFlatsCountBySiteQuery, Response<long>>
        {
            private readonly ILogger<FlatsGetFlatsCountBySiteHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetFlatsCountBySiteHandler(ILogger<FlatsGetFlatsCountBySiteHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(FlatsGetFlatsCountBySiteQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetFlatsCountBySiteAsync(request.FlatsSiteId, request.FlatsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}