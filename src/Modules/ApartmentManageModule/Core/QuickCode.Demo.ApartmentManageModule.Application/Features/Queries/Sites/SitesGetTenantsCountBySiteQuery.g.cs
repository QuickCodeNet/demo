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
    public class SitesGetTenantsCountBySiteQuery : IRequest<Response<long>>
    {
        public int SitesId { get; set; }
        public bool SitesIsActive { get; set; }
        public RelationshipType FlatContactsRelationshipType { get; set; }

        public SitesGetTenantsCountBySiteQuery(int sitesId, bool sitesIsActive, RelationshipType flatContactsRelationshipType)
        {
            this.SitesId = sitesId;
            this.SitesIsActive = sitesIsActive;
            this.FlatContactsRelationshipType = flatContactsRelationshipType;
        }

        public class SitesGetTenantsCountBySiteHandler : IRequestHandler<SitesGetTenantsCountBySiteQuery, Response<long>>
        {
            private readonly ILogger<SitesGetTenantsCountBySiteHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetTenantsCountBySiteHandler(ILogger<SitesGetTenantsCountBySiteHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(SitesGetTenantsCountBySiteQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetTenantsCountBySiteAsync(request.SitesId, request.SitesIsActive, request.FlatContactsRelationshipType);
                return returnValue.ToResponse();
            }
        }
    }
}