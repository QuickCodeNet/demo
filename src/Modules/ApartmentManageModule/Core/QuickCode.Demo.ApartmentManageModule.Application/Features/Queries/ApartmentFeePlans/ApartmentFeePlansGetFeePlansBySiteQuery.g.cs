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
    public class ApartmentFeePlansGetFeePlansBySiteQuery : IRequest<Response<List<ApartmentFeePlansGetFeePlansBySiteResponseDto>>>
    {
        public int ApartmentFeePlansSiteId { get; set; }
        public bool ApartmentFeePlansIsActive { get; set; }

        public ApartmentFeePlansGetFeePlansBySiteQuery(int apartmentFeePlansSiteId, bool apartmentFeePlansIsActive)
        {
            this.ApartmentFeePlansSiteId = apartmentFeePlansSiteId;
            this.ApartmentFeePlansIsActive = apartmentFeePlansIsActive;
        }

        public class ApartmentFeePlansGetFeePlansBySiteHandler : IRequestHandler<ApartmentFeePlansGetFeePlansBySiteQuery, Response<List<ApartmentFeePlansGetFeePlansBySiteResponseDto>>>
        {
            private readonly ILogger<ApartmentFeePlansGetFeePlansBySiteHandler> _logger;
            private readonly IApartmentFeePlansRepository _repository;
            public ApartmentFeePlansGetFeePlansBySiteHandler(ILogger<ApartmentFeePlansGetFeePlansBySiteHandler> logger, IApartmentFeePlansRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApartmentFeePlansGetFeePlansBySiteResponseDto>>> Handle(ApartmentFeePlansGetFeePlansBySiteQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentFeePlansGetFeePlansBySiteAsync(request.ApartmentFeePlansSiteId, request.ApartmentFeePlansIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}