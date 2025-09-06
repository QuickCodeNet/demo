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
    public class SitesGetFlatPaymentsForSitesQuery : IRequest<Response<List<SitesGetFlatPaymentsForSitesResponseDto>>>
    {
        public int SitesId { get; set; }

        public SitesGetFlatPaymentsForSitesQuery(int sitesId)
        {
            this.SitesId = sitesId;
        }

        public class SitesGetFlatPaymentsForSitesHandler : IRequestHandler<SitesGetFlatPaymentsForSitesQuery, Response<List<SitesGetFlatPaymentsForSitesResponseDto>>>
        {
            private readonly ILogger<SitesGetFlatPaymentsForSitesHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetFlatPaymentsForSitesHandler(ILogger<SitesGetFlatPaymentsForSitesHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SitesGetFlatPaymentsForSitesResponseDto>>> Handle(SitesGetFlatPaymentsForSitesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetFlatPaymentsForSitesAsync(request.SitesId);
                return returnValue.ToResponse();
            }
        }
    }
}