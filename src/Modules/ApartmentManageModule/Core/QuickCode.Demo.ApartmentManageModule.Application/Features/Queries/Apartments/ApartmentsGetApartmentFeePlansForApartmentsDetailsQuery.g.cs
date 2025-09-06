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
    public class ApartmentsGetApartmentFeePlansForApartmentsDetailsQuery : IRequest<Response<ApartmentsGetApartmentFeePlansForApartmentsResponseDto>>
    {
        public int ApartmentsId { get; set; }
        public int ApartmentFeePlansId { get; set; }

        public ApartmentsGetApartmentFeePlansForApartmentsDetailsQuery(int apartmentsId, int apartmentFeePlansId)
        {
            this.ApartmentsId = apartmentsId;
            this.ApartmentFeePlansId = apartmentFeePlansId;
        }

        public class ApartmentsGetApartmentFeePlansForApartmentsDetailsHandler : IRequestHandler<ApartmentsGetApartmentFeePlansForApartmentsDetailsQuery, Response<ApartmentsGetApartmentFeePlansForApartmentsResponseDto>>
        {
            private readonly ILogger<ApartmentsGetApartmentFeePlansForApartmentsDetailsHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetApartmentFeePlansForApartmentsDetailsHandler(ILogger<ApartmentsGetApartmentFeePlansForApartmentsDetailsHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApartmentsGetApartmentFeePlansForApartmentsResponseDto>> Handle(ApartmentsGetApartmentFeePlansForApartmentsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentsGetApartmentFeePlansForApartmentsDetailsAsync(request.ApartmentsId, request.ApartmentFeePlansId);
                return returnValue.ToResponse();
            }
        }
    }
}