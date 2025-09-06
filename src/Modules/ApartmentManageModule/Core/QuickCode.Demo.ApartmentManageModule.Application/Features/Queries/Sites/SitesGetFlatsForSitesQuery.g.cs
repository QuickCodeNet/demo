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
    public class SitesGetFlatsForSitesQuery : IRequest<Response<List<SitesGetFlatsForSitesResponseDto>>>
    {
        public int SitesId { get; set; }

        public SitesGetFlatsForSitesQuery(int sitesId)
        {
            this.SitesId = sitesId;
        }

        public class SitesGetFlatsForSitesHandler : IRequestHandler<SitesGetFlatsForSitesQuery, Response<List<SitesGetFlatsForSitesResponseDto>>>
        {
            private readonly ILogger<SitesGetFlatsForSitesHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetFlatsForSitesHandler(ILogger<SitesGetFlatsForSitesHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SitesGetFlatsForSitesResponseDto>>> Handle(SitesGetFlatsForSitesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetFlatsForSitesAsync(request.SitesId);
                return returnValue.ToResponse();
            }
        }
    }
}