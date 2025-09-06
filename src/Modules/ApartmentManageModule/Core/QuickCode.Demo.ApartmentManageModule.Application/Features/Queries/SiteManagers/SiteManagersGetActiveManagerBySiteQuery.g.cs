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
    public class SiteManagersGetActiveManagerBySiteQuery : IRequest<Response<SiteManagersGetActiveManagerBySiteResponseDto>>
    {
        public int SiteManagersSiteId { get; set; }
        public bool SiteManagersIsActive { get; set; }

        public SiteManagersGetActiveManagerBySiteQuery(int siteManagersSiteId, bool siteManagersIsActive)
        {
            this.SiteManagersSiteId = siteManagersSiteId;
            this.SiteManagersIsActive = siteManagersIsActive;
        }

        public class SiteManagersGetActiveManagerBySiteHandler : IRequestHandler<SiteManagersGetActiveManagerBySiteQuery, Response<SiteManagersGetActiveManagerBySiteResponseDto>>
        {
            private readonly ILogger<SiteManagersGetActiveManagerBySiteHandler> _logger;
            private readonly ISiteManagersRepository _repository;
            public SiteManagersGetActiveManagerBySiteHandler(ILogger<SiteManagersGetActiveManagerBySiteHandler> logger, ISiteManagersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SiteManagersGetActiveManagerBySiteResponseDto>> Handle(SiteManagersGetActiveManagerBySiteQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SiteManagersGetActiveManagerBySiteAsync(request.SiteManagersSiteId, request.SiteManagersIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}