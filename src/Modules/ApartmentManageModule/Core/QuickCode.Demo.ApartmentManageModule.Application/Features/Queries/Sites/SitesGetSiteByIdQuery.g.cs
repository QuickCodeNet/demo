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
    public class SitesGetSiteByIdQuery : IRequest<Response<SitesGetSiteByIdResponseDto>>
    {
        public int SitesId { get; set; }

        public SitesGetSiteByIdQuery(int sitesId)
        {
            this.SitesId = sitesId;
        }

        public class SitesGetSiteByIdHandler : IRequestHandler<SitesGetSiteByIdQuery, Response<SitesGetSiteByIdResponseDto>>
        {
            private readonly ILogger<SitesGetSiteByIdHandler> _logger;
            private readonly ISitesRepository _repository;
            public SitesGetSiteByIdHandler(ILogger<SitesGetSiteByIdHandler> logger, ISitesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<SitesGetSiteByIdResponseDto>> Handle(SitesGetSiteByIdQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.SitesGetSiteByIdAsync(request.SitesId);
                return returnValue.ToResponse();
            }
        }
    }
}