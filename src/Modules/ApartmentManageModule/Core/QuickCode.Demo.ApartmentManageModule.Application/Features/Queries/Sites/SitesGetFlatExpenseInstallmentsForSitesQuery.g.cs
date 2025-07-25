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
    public class SitesGetFlatExpenseInstallmentsForSitesQuery : IRequest<Response<List<SitesGetFlatExpenseInstallmentsForSitesResponseDto>>>
    {
        public int SitesId { get; set; }

        public SitesGetFlatExpenseInstallmentsForSitesQuery(int sitesId)
        {
            this.SitesId = sitesId;
        }

        public class SitesGetFlatExpenseInstallmentsForSitesHandler : IRequestHandler<SitesGetFlatExpenseInstallmentsForSitesQuery, Response<List<SitesGetFlatExpenseInstallmentsForSitesResponseDto>>>
        {
            private readonly ILogger<SitesGetFlatExpenseInstallmentsForSitesHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetFlatExpenseInstallmentsForSitesHandler(ILogger<SitesGetFlatExpenseInstallmentsForSitesHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SitesGetFlatExpenseInstallmentsForSitesResponseDto>>> Handle(SitesGetFlatExpenseInstallmentsForSitesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetFlatExpenseInstallmentsForSitesAsync(request.SitesId);
                return returnValue.ToResponse();
            }
        }
    }
}