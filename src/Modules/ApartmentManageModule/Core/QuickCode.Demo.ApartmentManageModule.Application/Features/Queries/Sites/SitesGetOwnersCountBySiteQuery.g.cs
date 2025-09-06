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
    public class SitesGetOwnersCountBySiteQuery : IRequest<Response<long>>
    {
        public int SitesId { get; set; }
        public bool SitesIsActive { get; set; }
        public RelationshipType FlatContactsRelationshipType { get; set; }

        public SitesGetOwnersCountBySiteQuery(int sitesId, bool sitesIsActive, RelationshipType flatContactsRelationshipType)
        {
            this.SitesId = sitesId;
            this.SitesIsActive = sitesIsActive;
            this.FlatContactsRelationshipType = flatContactsRelationshipType;
        }

        public class SitesGetOwnersCountBySiteHandler : IRequestHandler<SitesGetOwnersCountBySiteQuery, Response<long>>
        {
            private readonly ILogger<SitesGetOwnersCountBySiteHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetOwnersCountBySiteHandler(ILogger<SitesGetOwnersCountBySiteHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(SitesGetOwnersCountBySiteQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetOwnersCountBySiteAsync(request.SitesId, request.SitesIsActive, request.FlatContactsRelationshipType);
                return returnValue.ToResponse();
            }
        }
    }
}