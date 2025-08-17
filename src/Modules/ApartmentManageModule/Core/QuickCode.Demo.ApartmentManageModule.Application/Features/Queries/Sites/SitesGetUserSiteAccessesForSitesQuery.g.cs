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
    public class SitesGetUserSiteAccessesForSitesQuery : IRequest<Response<List<SitesGetUserSiteAccessesForSitesResponseDto>>>
    {
        public int SitesId { get; set; }

        public SitesGetUserSiteAccessesForSitesQuery(int sitesId)
        {
            this.SitesId = sitesId;
        }

        public class SitesGetUserSiteAccessesForSitesHandler : IRequestHandler<SitesGetUserSiteAccessesForSitesQuery, Response<List<SitesGetUserSiteAccessesForSitesResponseDto>>>
        {
            private readonly ILogger<SitesGetUserSiteAccessesForSitesHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetUserSiteAccessesForSitesHandler(ILogger<SitesGetUserSiteAccessesForSitesHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SitesGetUserSiteAccessesForSitesResponseDto>>> Handle(SitesGetUserSiteAccessesForSitesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetUserSiteAccessesForSitesAsync(request.SitesId);
                return returnValue.ToResponse();
            }
        }
    }
}