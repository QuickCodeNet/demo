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
    public class SitesGetExpenseInstallmentsForSitesQuery : IRequest<Response<List<SitesGetExpenseInstallmentsForSitesResponseDto>>>
    {
        public int SitesId { get; set; }

        public SitesGetExpenseInstallmentsForSitesQuery(int sitesId)
        {
            this.SitesId = sitesId;
        }

        public class SitesGetExpenseInstallmentsForSitesHandler : IRequestHandler<SitesGetExpenseInstallmentsForSitesQuery, Response<List<SitesGetExpenseInstallmentsForSitesResponseDto>>>
        {
            private readonly ILogger<SitesGetExpenseInstallmentsForSitesHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetExpenseInstallmentsForSitesHandler(ILogger<SitesGetExpenseInstallmentsForSitesHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SitesGetExpenseInstallmentsForSitesResponseDto>>> Handle(SitesGetExpenseInstallmentsForSitesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetExpenseInstallmentsForSitesAsync(request.SitesId);
                return returnValue.ToResponse();
            }
        }
    }
}