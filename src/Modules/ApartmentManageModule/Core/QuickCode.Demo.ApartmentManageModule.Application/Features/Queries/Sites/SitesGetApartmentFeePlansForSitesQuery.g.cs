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
    public class SitesGetApartmentFeePlansForSitesQuery : IRequest<Response<List<SitesGetApartmentFeePlansForSitesResponseDto>>>
    {
        public int SitesId { get; set; }

        public SitesGetApartmentFeePlansForSitesQuery(int sitesId)
        {
            this.SitesId = sitesId;
        }

        public class SitesGetApartmentFeePlansForSitesHandler : IRequestHandler<SitesGetApartmentFeePlansForSitesQuery, Response<List<SitesGetApartmentFeePlansForSitesResponseDto>>>
        {
            private readonly ILogger<SitesGetApartmentFeePlansForSitesHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetApartmentFeePlansForSitesHandler(ILogger<SitesGetApartmentFeePlansForSitesHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SitesGetApartmentFeePlansForSitesResponseDto>>> Handle(SitesGetApartmentFeePlansForSitesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetApartmentFeePlansForSitesAsync(request.SitesId);
                return returnValue.ToResponse();
            }
        }
    }
}