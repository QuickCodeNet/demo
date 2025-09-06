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
    public class SiteManagersCheckSiteHasManagerQuery : IRequest<Response<bool>>
    {
        public int SiteManagersSiteId { get; set; }
        public bool SiteManagersIsActive { get; set; }

        public SiteManagersCheckSiteHasManagerQuery(int siteManagersSiteId, bool siteManagersIsActive)
        {
            this.SiteManagersSiteId = siteManagersSiteId;
            this.SiteManagersIsActive = siteManagersIsActive;
        }

        public class SiteManagersCheckSiteHasManagerHandler : IRequestHandler<SiteManagersCheckSiteHasManagerQuery, Response<bool>>
        {
            private readonly ILogger<SiteManagersCheckSiteHasManagerHandler> _logger;
            private readonly ISiteManagersRepository _repository;
            public SiteManagersCheckSiteHasManagerHandler(ILogger<SiteManagersCheckSiteHasManagerHandler> logger, ISiteManagersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(SiteManagersCheckSiteHasManagerQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SiteManagersCheckSiteHasManagerAsync(request.SiteManagersSiteId, request.SiteManagersIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}