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
    public class SitesGetUserSiteAccessesForSitesDetailsQuery : IRequest<Response<SitesGetUserSiteAccessesForSitesResponseDto>>
    {
        public int SitesId { get; set; }
        public int UserSiteAccessesId { get; set; }

        public SitesGetUserSiteAccessesForSitesDetailsQuery(int sitesId, int userSiteAccessesId)
        {
            this.SitesId = sitesId;
            this.UserSiteAccessesId = userSiteAccessesId;
        }

        public class SitesGetUserSiteAccessesForSitesDetailsHandler : IRequestHandler<SitesGetUserSiteAccessesForSitesDetailsQuery, Response<SitesGetUserSiteAccessesForSitesResponseDto>>
        {
            private readonly ILogger<SitesGetUserSiteAccessesForSitesDetailsHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetUserSiteAccessesForSitesDetailsHandler(ILogger<SitesGetUserSiteAccessesForSitesDetailsHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SitesGetUserSiteAccessesForSitesResponseDto>> Handle(SitesGetUserSiteAccessesForSitesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetUserSiteAccessesForSitesDetailsAsync(request.SitesId, request.UserSiteAccessesId);
                return returnValue.ToResponse();
            }
        }
    }
}