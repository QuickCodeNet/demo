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
    public class SitesGetCommonExpensesForSitesQuery : IRequest<Response<List<SitesGetCommonExpensesForSitesResponseDto>>>
    {
        public int SitesId { get; set; }

        public SitesGetCommonExpensesForSitesQuery(int sitesId)
        {
            this.SitesId = sitesId;
        }

        public class SitesGetCommonExpensesForSitesHandler : IRequestHandler<SitesGetCommonExpensesForSitesQuery, Response<List<SitesGetCommonExpensesForSitesResponseDto>>>
        {
            private readonly ILogger<SitesGetCommonExpensesForSitesHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetCommonExpensesForSitesHandler(ILogger<SitesGetCommonExpensesForSitesHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<SitesGetCommonExpensesForSitesResponseDto>>> Handle(SitesGetCommonExpensesForSitesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetCommonExpensesForSitesAsync(request.SitesId);
                return returnValue.ToResponse();
            }
        }
    }
}