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
    public class SitesGetFlatPaymentsForSitesDetailsQuery : IRequest<Response<SitesGetFlatPaymentsForSitesResponseDto>>
    {
        public int SitesId { get; set; }
        public int FlatPaymentsId { get; set; }

        public SitesGetFlatPaymentsForSitesDetailsQuery(int sitesId, int flatPaymentsId)
        {
            this.SitesId = sitesId;
            this.FlatPaymentsId = flatPaymentsId;
        }

        public class SitesGetFlatPaymentsForSitesDetailsHandler : IRequestHandler<SitesGetFlatPaymentsForSitesDetailsQuery, Response<SitesGetFlatPaymentsForSitesResponseDto>>
        {
            private readonly ILogger<SitesGetFlatPaymentsForSitesDetailsHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetFlatPaymentsForSitesDetailsHandler(ILogger<SitesGetFlatPaymentsForSitesDetailsHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SitesGetFlatPaymentsForSitesResponseDto>> Handle(SitesGetFlatPaymentsForSitesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetFlatPaymentsForSitesDetailsAsync(request.SitesId, request.FlatPaymentsId);
                return returnValue.ToResponse();
            }
        }
    }
}