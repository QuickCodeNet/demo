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
    public class SitesGetApartmentsForSitesDetailsQuery : IRequest<Response<SitesGetApartmentsForSitesResponseDto>>
    {
        public int SitesId { get; set; }
        public int ApartmentsId { get; set; }

        public SitesGetApartmentsForSitesDetailsQuery(int sitesId, int apartmentsId)
        {
            this.SitesId = sitesId;
            this.ApartmentsId = apartmentsId;
        }

        public class SitesGetApartmentsForSitesDetailsHandler : IRequestHandler<SitesGetApartmentsForSitesDetailsQuery, Response<SitesGetApartmentsForSitesResponseDto>>
        {
            private readonly ILogger<SitesGetApartmentsForSitesDetailsHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetApartmentsForSitesDetailsHandler(ILogger<SitesGetApartmentsForSitesDetailsHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SitesGetApartmentsForSitesResponseDto>> Handle(SitesGetApartmentsForSitesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetApartmentsForSitesDetailsAsync(request.SitesId, request.ApartmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}