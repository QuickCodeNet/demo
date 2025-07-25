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
    public class ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansQuery : IRequest<Response<List<ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansResponseDto>>>
    {
        public int ApartmentFeePlansId { get; set; }

        public ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansQuery(int apartmentFeePlansId)
        {
            this.ApartmentFeePlansId = apartmentFeePlansId;
        }

        public class ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansHandler : IRequestHandler<ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansQuery, Response<List<ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansResponseDto>>>
        {
            private readonly ILogger<ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansHandler> _logger;
            private readonly IApartmentFeePlansRepository _repository;
            public ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansHandler(ILogger<ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansHandler> logger, IApartmentFeePlansRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansResponseDto>>> Handle(ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansAsync(request.ApartmentFeePlansId);
                return returnValue.ToResponse();
            }
        }
    }
}