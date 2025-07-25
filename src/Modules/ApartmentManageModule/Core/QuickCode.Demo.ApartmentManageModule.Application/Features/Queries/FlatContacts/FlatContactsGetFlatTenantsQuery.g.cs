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
    public class FlatContactsGetFlatTenantsQuery : IRequest<Response<List<FlatContactsGetFlatTenantsResponseDto>>>
    {
        public int FlatContactsFlatId { get; set; }
        public RelationshipType FlatContactsRelationshipType { get; set; }
        public bool FlatContactsIsActive { get; set; }

        public FlatContactsGetFlatTenantsQuery(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            this.FlatContactsFlatId = flatContactsFlatId;
            this.FlatContactsRelationshipType = flatContactsRelationshipType;
            this.FlatContactsIsActive = flatContactsIsActive;
        }

        public class FlatContactsGetFlatTenantsHandler : IRequestHandler<FlatContactsGetFlatTenantsQuery, Response<List<FlatContactsGetFlatTenantsResponseDto>>>
        {
            private readonly ILogger<FlatContactsGetFlatTenantsHandler> _logger;
            private readonly IFlatContactsRepository _repository;
            public FlatContactsGetFlatTenantsHandler(ILogger<FlatContactsGetFlatTenantsHandler> logger, IFlatContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatContactsGetFlatTenantsResponseDto>>> Handle(FlatContactsGetFlatTenantsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatContactsGetFlatTenantsAsync(request.FlatContactsFlatId, request.FlatContactsRelationshipType, request.FlatContactsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}