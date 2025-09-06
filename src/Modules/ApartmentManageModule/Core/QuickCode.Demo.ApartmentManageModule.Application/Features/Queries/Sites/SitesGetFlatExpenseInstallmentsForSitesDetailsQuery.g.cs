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
    public class SitesGetFlatExpenseInstallmentsForSitesDetailsQuery : IRequest<Response<SitesGetFlatExpenseInstallmentsForSitesResponseDto>>
    {
        public int SitesId { get; set; }
        public int FlatExpenseInstallmentsId { get; set; }

        public SitesGetFlatExpenseInstallmentsForSitesDetailsQuery(int sitesId, int flatExpenseInstallmentsId)
        {
            this.SitesId = sitesId;
            this.FlatExpenseInstallmentsId = flatExpenseInstallmentsId;
        }

        public class SitesGetFlatExpenseInstallmentsForSitesDetailsHandler : IRequestHandler<SitesGetFlatExpenseInstallmentsForSitesDetailsQuery, Response<SitesGetFlatExpenseInstallmentsForSitesResponseDto>>
        {
            private readonly ILogger<SitesGetFlatExpenseInstallmentsForSitesDetailsHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetFlatExpenseInstallmentsForSitesDetailsHandler(ILogger<SitesGetFlatExpenseInstallmentsForSitesDetailsHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SitesGetFlatExpenseInstallmentsForSitesResponseDto>> Handle(SitesGetFlatExpenseInstallmentsForSitesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetFlatExpenseInstallmentsForSitesDetailsAsync(request.SitesId, request.FlatExpenseInstallmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}