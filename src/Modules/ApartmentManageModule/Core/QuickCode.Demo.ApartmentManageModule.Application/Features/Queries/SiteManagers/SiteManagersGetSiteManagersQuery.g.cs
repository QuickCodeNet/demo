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
    public class SiteManagersGetSiteManagersQuery : IRequest<Response<List<SiteManagersGetSiteManagersResponseDto>>>
    {
        public int SiteManagersSiteId { get; set; }
        public bool SiteManagersIsActive { get; set; }

        public SiteManagersGetSiteManagersQuery(int siteManagersSiteId, bool siteManagersIsActive)
        {
            this.SiteManagersSiteId = siteManagersSiteId;
            this.SiteManagersIsActive = siteManagersIsActive;
        }

        public class SiteManagersGetSiteManagersHandler : IRequestHandler<SiteManagersGetSiteManagersQuery, Response<List<SiteManagersGetSiteManagersResponseDto>>>
        {
            private readonly ILogger<SiteManagersGetSiteManagersHandler> _logger;
            private readonly ISiteManagersRepository _repository;
            public SiteManagersGetSiteManagersHandler(ILogger<SiteManagersGetSiteManagersHandler> logger, ISiteManagersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SiteManagersGetSiteManagersResponseDto>>> Handle(SiteManagersGetSiteManagersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SiteManagersGetSiteManagersAsync(request.SiteManagersSiteId, request.SiteManagersIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}