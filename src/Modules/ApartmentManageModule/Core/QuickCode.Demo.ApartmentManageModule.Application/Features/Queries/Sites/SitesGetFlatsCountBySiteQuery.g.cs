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
    public class SitesGetFlatsCountBySiteQuery : IRequest<Response<long>>
    {
        public int SitesId { get; set; }
        public bool SitesIsActive { get; set; }

        public SitesGetFlatsCountBySiteQuery(int sitesId, bool sitesIsActive)
        {
            this.SitesId = sitesId;
            this.SitesIsActive = sitesIsActive;
        }

        public class SitesGetFlatsCountBySiteHandler : IRequestHandler<SitesGetFlatsCountBySiteQuery, Response<long>>
        {
            private readonly ILogger<SitesGetFlatsCountBySiteHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetFlatsCountBySiteHandler(ILogger<SitesGetFlatsCountBySiteHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(SitesGetFlatsCountBySiteQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetFlatsCountBySiteAsync(request.SitesId, request.SitesIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}