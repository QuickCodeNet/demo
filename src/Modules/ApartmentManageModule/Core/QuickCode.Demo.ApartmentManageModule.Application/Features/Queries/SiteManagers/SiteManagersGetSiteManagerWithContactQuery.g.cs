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
    public class SiteManagersGetSiteManagerWithContactQuery : IRequest<Response<List<SiteManagersGetSiteManagerWithContactResponseDto>>>
    {
        public int SiteManagersContactId { get; set; }

        public SiteManagersGetSiteManagerWithContactQuery(int siteManagersContactId)
        {
            this.SiteManagersContactId = siteManagersContactId;
        }

        public class SiteManagersGetSiteManagerWithContactHandler : IRequestHandler<SiteManagersGetSiteManagerWithContactQuery, Response<List<SiteManagersGetSiteManagerWithContactResponseDto>>>
        {
            private readonly ILogger<SiteManagersGetSiteManagerWithContactHandler> _logger;
            private readonly ISiteManagersRepository _repository;
            public SiteManagersGetSiteManagerWithContactHandler(ILogger<SiteManagersGetSiteManagerWithContactHandler> logger, ISiteManagersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SiteManagersGetSiteManagerWithContactResponseDto>>> Handle(SiteManagersGetSiteManagerWithContactQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SiteManagersGetSiteManagerWithContactAsync(request.SiteManagersContactId);
                return returnValue.ToResponse();
            }
        }
    }
}