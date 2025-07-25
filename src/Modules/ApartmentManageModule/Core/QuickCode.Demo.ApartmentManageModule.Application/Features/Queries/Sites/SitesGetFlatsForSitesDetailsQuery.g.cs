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
    public class SitesGetFlatsForSitesDetailsQuery : IRequest<Response<SitesGetFlatsForSitesResponseDto>>
    {
        public int SitesId { get; set; }
        public int FlatsId { get; set; }

        public SitesGetFlatsForSitesDetailsQuery(int sitesId, int flatsId)
        {
            this.SitesId = sitesId;
            this.FlatsId = flatsId;
        }

        public class SitesGetFlatsForSitesDetailsHandler : IRequestHandler<SitesGetFlatsForSitesDetailsQuery, Response<SitesGetFlatsForSitesResponseDto>>
        {
            private readonly ILogger<SitesGetFlatsForSitesDetailsHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetFlatsForSitesDetailsHandler(ILogger<SitesGetFlatsForSitesDetailsHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SitesGetFlatsForSitesResponseDto>> Handle(SitesGetFlatsForSitesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetFlatsForSitesDetailsAsync(request.SitesId, request.FlatsId);
                return returnValue.ToResponse();
            }
        }
    }
}