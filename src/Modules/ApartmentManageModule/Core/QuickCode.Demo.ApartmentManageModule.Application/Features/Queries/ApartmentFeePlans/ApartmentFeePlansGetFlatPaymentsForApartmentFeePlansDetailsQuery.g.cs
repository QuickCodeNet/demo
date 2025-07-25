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
    public class ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansDetailsQuery : IRequest<Response<ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansResponseDto>>
    {
        public int ApartmentFeePlansId { get; set; }
        public int FlatPaymentsId { get; set; }

        public ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansDetailsQuery(int apartmentFeePlansId, int flatPaymentsId)
        {
            this.ApartmentFeePlansId = apartmentFeePlansId;
            this.FlatPaymentsId = flatPaymentsId;
        }

        public class ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansDetailsHandler : IRequestHandler<ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansDetailsQuery, Response<ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansResponseDto>>
        {
            private readonly ILogger<ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansDetailsHandler> _logger;
            private readonly IApartmentFeePlansRepository _repository;
            public ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansDetailsHandler(ILogger<ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansDetailsHandler> logger, IApartmentFeePlansRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansResponseDto>> Handle(ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentFeePlansGetFlatPaymentsForApartmentFeePlansDetailsAsync(request.ApartmentFeePlansId, request.FlatPaymentsId);
                return returnValue.ToResponse();
            }
        }
    }
}