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
    public class ApartmentsGetApartmentsBySiteQuery : IRequest<Response<List<ApartmentsGetApartmentsBySiteResponseDto>>>
    {
        public int ApartmentsSiteId { get; set; }
        public bool ApartmentsIsActive { get; set; }

        public ApartmentsGetApartmentsBySiteQuery(int apartmentsSiteId, bool apartmentsIsActive)
        {
            this.ApartmentsSiteId = apartmentsSiteId;
            this.ApartmentsIsActive = apartmentsIsActive;
        }

        public class ApartmentsGetApartmentsBySiteHandler : IRequestHandler<ApartmentsGetApartmentsBySiteQuery, Response<List<ApartmentsGetApartmentsBySiteResponseDto>>>
        {
            private readonly ILogger<ApartmentsGetApartmentsBySiteHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetApartmentsBySiteHandler(ILogger<ApartmentsGetApartmentsBySiteHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApartmentsGetApartmentsBySiteResponseDto>>> Handle(ApartmentsGetApartmentsBySiteQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentsGetApartmentsBySiteAsync(request.ApartmentsSiteId, request.ApartmentsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}