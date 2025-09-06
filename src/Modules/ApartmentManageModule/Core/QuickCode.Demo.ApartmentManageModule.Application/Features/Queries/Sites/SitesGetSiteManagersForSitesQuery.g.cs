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
    public class SitesGetSiteManagersForSitesQuery : IRequest<Response<List<SitesGetSiteManagersForSitesResponseDto>>>
    {
        public int SitesId { get; set; }

        public SitesGetSiteManagersForSitesQuery(int sitesId)
        {
            this.SitesId = sitesId;
        }

        public class SitesGetSiteManagersForSitesHandler : IRequestHandler<SitesGetSiteManagersForSitesQuery, Response<List<SitesGetSiteManagersForSitesResponseDto>>>
        {
            private readonly ILogger<SitesGetSiteManagersForSitesHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetSiteManagersForSitesHandler(ILogger<SitesGetSiteManagersForSitesHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SitesGetSiteManagersForSitesResponseDto>>> Handle(SitesGetSiteManagersForSitesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetSiteManagersForSitesAsync(request.SitesId);
                return returnValue.ToResponse();
            }
        }
    }
}