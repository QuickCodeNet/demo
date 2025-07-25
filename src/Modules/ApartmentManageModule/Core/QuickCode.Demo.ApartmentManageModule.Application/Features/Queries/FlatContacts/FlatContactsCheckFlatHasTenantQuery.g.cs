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
    public class FlatContactsCheckFlatHasTenantQuery : IRequest<Response<bool>>
    {
        public int FlatContactsFlatId { get; set; }
        public RelationshipType FlatContactsRelationshipType { get; set; }
        public bool FlatContactsIsActive { get; set; }

        public FlatContactsCheckFlatHasTenantQuery(int flatContactsFlatId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            this.FlatContactsFlatId = flatContactsFlatId;
            this.FlatContactsRelationshipType = flatContactsRelationshipType;
            this.FlatContactsIsActive = flatContactsIsActive;
        }

        public class FlatContactsCheckFlatHasTenantHandler : IRequestHandler<FlatContactsCheckFlatHasTenantQuery, Response<bool>>
        {
            private readonly ILogger<FlatContactsCheckFlatHasTenantHandler> _logger;
            private readonly IFlatContactsRepository _repository;
            public FlatContactsCheckFlatHasTenantHandler(ILogger<FlatContactsCheckFlatHasTenantHandler> logger, IFlatContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(FlatContactsCheckFlatHasTenantQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatContactsCheckFlatHasTenantAsync(request.FlatContactsFlatId, request.FlatContactsRelationshipType, request.FlatContactsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}