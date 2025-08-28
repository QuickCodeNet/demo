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
    public class FlatContactsGetContactOwnedFlatsQuery : IRequest<Response<List<FlatContactsGetContactOwnedFlatsResponseDto>>>
    {
        public int FlatContactsContactId { get; set; }
        public RelationshipType FlatContactsRelationshipType { get; set; }
        public bool FlatContactsIsActive { get; set; }

        public FlatContactsGetContactOwnedFlatsQuery(int flatContactsContactId, RelationshipType flatContactsRelationshipType, bool flatContactsIsActive)
        {
            this.FlatContactsContactId = flatContactsContactId;
            this.FlatContactsRelationshipType = flatContactsRelationshipType;
            this.FlatContactsIsActive = flatContactsIsActive;
        }

        public class FlatContactsGetContactOwnedFlatsHandler : IRequestHandler<FlatContactsGetContactOwnedFlatsQuery, Response<List<FlatContactsGetContactOwnedFlatsResponseDto>>>
        {
            private readonly ILogger<FlatContactsGetContactOwnedFlatsHandler> _logger;
            private readonly IFlatContactsRepository _repository;
            public FlatContactsGetContactOwnedFlatsHandler(ILogger<FlatContactsGetContactOwnedFlatsHandler> logger, IFlatContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatContactsGetContactOwnedFlatsResponseDto>>> Handle(FlatContactsGetContactOwnedFlatsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatContactsGetContactOwnedFlatsAsync(request.FlatContactsContactId, request.FlatContactsRelationshipType, request.FlatContactsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}