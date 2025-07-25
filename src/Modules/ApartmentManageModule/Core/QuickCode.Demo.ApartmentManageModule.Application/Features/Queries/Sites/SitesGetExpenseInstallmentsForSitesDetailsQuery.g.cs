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
    public class SitesGetExpenseInstallmentsForSitesDetailsQuery : IRequest<Response<SitesGetExpenseInstallmentsForSitesResponseDto>>
    {
        public int SitesId { get; set; }
        public int ExpenseInstallmentsId { get; set; }

        public SitesGetExpenseInstallmentsForSitesDetailsQuery(int sitesId, int expenseInstallmentsId)
        {
            this.SitesId = sitesId;
            this.ExpenseInstallmentsId = expenseInstallmentsId;
        }

        public class SitesGetExpenseInstallmentsForSitesDetailsHandler : IRequestHandler<SitesGetExpenseInstallmentsForSitesDetailsQuery, Response<SitesGetExpenseInstallmentsForSitesResponseDto>>
        {
            private readonly ILogger<SitesGetExpenseInstallmentsForSitesDetailsHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetExpenseInstallmentsForSitesDetailsHandler(ILogger<SitesGetExpenseInstallmentsForSitesDetailsHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SitesGetExpenseInstallmentsForSitesResponseDto>> Handle(SitesGetExpenseInstallmentsForSitesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetExpenseInstallmentsForSitesDetailsAsync(request.SitesId, request.ExpenseInstallmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}