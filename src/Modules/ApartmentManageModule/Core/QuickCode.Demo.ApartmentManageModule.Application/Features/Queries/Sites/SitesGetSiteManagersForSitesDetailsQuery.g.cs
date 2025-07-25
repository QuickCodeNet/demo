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
    public class SitesGetSiteManagersForSitesDetailsQuery : IRequest<Response<SitesGetSiteManagersForSitesResponseDto>>
    {
        public int SitesId { get; set; }
        public int SiteManagersId { get; set; }

        public SitesGetSiteManagersForSitesDetailsQuery(int sitesId, int siteManagersId)
        {
            this.SitesId = sitesId;
            this.SiteManagersId = siteManagersId;
        }

        public class SitesGetSiteManagersForSitesDetailsHandler : IRequestHandler<SitesGetSiteManagersForSitesDetailsQuery, Response<SitesGetSiteManagersForSitesResponseDto>>
        {
            private readonly ILogger<SitesGetSiteManagersForSitesDetailsHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetSiteManagersForSitesDetailsHandler(ILogger<SitesGetSiteManagersForSitesDetailsHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SitesGetSiteManagersForSitesResponseDto>> Handle(SitesGetSiteManagersForSitesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetSiteManagersForSitesDetailsAsync(request.SitesId, request.SiteManagersId);
                return returnValue.ToResponse();
            }
        }
    }
}