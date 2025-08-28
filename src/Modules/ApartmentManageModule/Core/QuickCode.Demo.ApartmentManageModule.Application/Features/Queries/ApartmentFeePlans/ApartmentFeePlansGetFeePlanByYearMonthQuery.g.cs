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
    public class ApartmentFeePlansGetFeePlanByYearMonthQuery : IRequest<Response<List<ApartmentFeePlansGetFeePlanByYearMonthResponseDto>>>
    {
        public int ApartmentFeePlansSiteId { get; set; }
        public int ApartmentFeePlansApartmentId { get; set; }
        public int ApartmentFeePlansYearId { get; set; }
        public int ApartmentFeePlansMonthId { get; set; }
        public bool ApartmentFeePlansIsActive { get; set; }

        public ApartmentFeePlansGetFeePlanByYearMonthQuery(int apartmentFeePlansSiteId, int apartmentFeePlansApartmentId, int apartmentFeePlansYearId, int apartmentFeePlansMonthId, bool apartmentFeePlansIsActive)
        {
            this.ApartmentFeePlansSiteId = apartmentFeePlansSiteId;
            this.ApartmentFeePlansApartmentId = apartmentFeePlansApartmentId;
            this.ApartmentFeePlansYearId = apartmentFeePlansYearId;
            this.ApartmentFeePlansMonthId = apartmentFeePlansMonthId;
            this.ApartmentFeePlansIsActive = apartmentFeePlansIsActive;
        }

        public class ApartmentFeePlansGetFeePlanByYearMonthHandler : IRequestHandler<ApartmentFeePlansGetFeePlanByYearMonthQuery, Response<List<ApartmentFeePlansGetFeePlanByYearMonthResponseDto>>>
        {
            private readonly ILogger<ApartmentFeePlansGetFeePlanByYearMonthHandler> _logger;
            private readonly IApartmentFeePlansRepository _repository;
            public ApartmentFeePlansGetFeePlanByYearMonthHandler(ILogger<ApartmentFeePlansGetFeePlanByYearMonthHandler> logger, IApartmentFeePlansRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApartmentFeePlansGetFeePlanByYearMonthResponseDto>>> Handle(ApartmentFeePlansGetFeePlanByYearMonthQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentFeePlansGetFeePlanByYearMonthAsync(request.ApartmentFeePlansSiteId, request.ApartmentFeePlansApartmentId, request.ApartmentFeePlansYearId, request.ApartmentFeePlansMonthId, request.ApartmentFeePlansIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}