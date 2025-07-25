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
    public class FlatsGetUserSiteAccessesForFlatsQuery : IRequest<Response<List<FlatsGetUserSiteAccessesForFlatsResponseDto>>>
    {
        public int FlatsId { get; set; }

        public FlatsGetUserSiteAccessesForFlatsQuery(int flatsId)
        {
            this.FlatsId = flatsId;
        }

        public class FlatsGetUserSiteAccessesForFlatsHandler : IRequestHandler<FlatsGetUserSiteAccessesForFlatsQuery, Response<List<FlatsGetUserSiteAccessesForFlatsResponseDto>>>
        {
            private readonly ILogger<FlatsGetUserSiteAccessesForFlatsHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetUserSiteAccessesForFlatsHandler(ILogger<FlatsGetUserSiteAccessesForFlatsHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatsGetUserSiteAccessesForFlatsResponseDto>>> Handle(FlatsGetUserSiteAccessesForFlatsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetUserSiteAccessesForFlatsAsync(request.FlatsId);
                return returnValue.ToResponse();
            }
        }
    }
}