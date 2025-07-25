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
    public class FlatsGetUserSiteAccessesForFlatsDetailsQuery : IRequest<Response<FlatsGetUserSiteAccessesForFlatsResponseDto>>
    {
        public int FlatsId { get; set; }
        public int UserSiteAccessesId { get; set; }

        public FlatsGetUserSiteAccessesForFlatsDetailsQuery(int flatsId, int userSiteAccessesId)
        {
            this.FlatsId = flatsId;
            this.UserSiteAccessesId = userSiteAccessesId;
        }

        public class FlatsGetUserSiteAccessesForFlatsDetailsHandler : IRequestHandler<FlatsGetUserSiteAccessesForFlatsDetailsQuery, Response<FlatsGetUserSiteAccessesForFlatsResponseDto>>
        {
            private readonly ILogger<FlatsGetUserSiteAccessesForFlatsDetailsHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetUserSiteAccessesForFlatsDetailsHandler(ILogger<FlatsGetUserSiteAccessesForFlatsDetailsHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FlatsGetUserSiteAccessesForFlatsResponseDto>> Handle(FlatsGetUserSiteAccessesForFlatsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetUserSiteAccessesForFlatsDetailsAsync(request.FlatsId, request.UserSiteAccessesId);
                return returnValue.ToResponse();
            }
        }
    }
}