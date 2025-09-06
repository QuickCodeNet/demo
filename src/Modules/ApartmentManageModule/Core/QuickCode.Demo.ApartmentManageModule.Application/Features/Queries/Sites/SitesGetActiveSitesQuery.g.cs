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
    public class SitesGetActiveSitesQuery : IRequest<Response<List<SitesGetActiveSitesResponseDto>>>
    {
        public bool SitesIsActive { get; set; }

        public SitesGetActiveSitesQuery(bool sitesIsActive)
        {
            this.SitesIsActive = sitesIsActive;
        }

        public class SitesGetActiveSitesHandler : IRequestHandler<SitesGetActiveSitesQuery, Response<List<SitesGetActiveSitesResponseDto>>>
        {
            private readonly ILogger<SitesGetActiveSitesHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetActiveSitesHandler(ILogger<SitesGetActiveSitesHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SitesGetActiveSitesResponseDto>>> Handle(SitesGetActiveSitesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetActiveSitesAsync(request.SitesIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}