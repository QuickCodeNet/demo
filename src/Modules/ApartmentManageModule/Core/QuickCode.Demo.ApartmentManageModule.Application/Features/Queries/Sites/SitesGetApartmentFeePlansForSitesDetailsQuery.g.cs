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
    public class SitesGetApartmentFeePlansForSitesDetailsQuery : IRequest<Response<SitesGetApartmentFeePlansForSitesResponseDto>>
    {
        public int SitesId { get; set; }
        public int ApartmentFeePlansId { get; set; }

        public SitesGetApartmentFeePlansForSitesDetailsQuery(int sitesId, int apartmentFeePlansId)
        {
            this.SitesId = sitesId;
            this.ApartmentFeePlansId = apartmentFeePlansId;
        }

        public class SitesGetApartmentFeePlansForSitesDetailsHandler : IRequestHandler<SitesGetApartmentFeePlansForSitesDetailsQuery, Response<SitesGetApartmentFeePlansForSitesResponseDto>>
        {
            private readonly ILogger<SitesGetApartmentFeePlansForSitesDetailsHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetApartmentFeePlansForSitesDetailsHandler(ILogger<SitesGetApartmentFeePlansForSitesDetailsHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SitesGetApartmentFeePlansForSitesResponseDto>> Handle(SitesGetApartmentFeePlansForSitesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetApartmentFeePlansForSitesDetailsAsync(request.SitesId, request.ApartmentFeePlansId);
                return returnValue.ToResponse();
            }
        }
    }
}