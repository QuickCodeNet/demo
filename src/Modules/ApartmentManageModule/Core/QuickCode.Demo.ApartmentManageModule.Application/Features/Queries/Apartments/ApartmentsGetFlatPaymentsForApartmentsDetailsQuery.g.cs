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
    public class ApartmentsGetFlatPaymentsForApartmentsDetailsQuery : IRequest<Response<ApartmentsGetFlatPaymentsForApartmentsResponseDto>>
    {
        public int ApartmentsId { get; set; }
        public int FlatPaymentsId { get; set; }

        public ApartmentsGetFlatPaymentsForApartmentsDetailsQuery(int apartmentsId, int flatPaymentsId)
        {
            this.ApartmentsId = apartmentsId;
            this.FlatPaymentsId = flatPaymentsId;
        }

        public class ApartmentsGetFlatPaymentsForApartmentsDetailsHandler : IRequestHandler<ApartmentsGetFlatPaymentsForApartmentsDetailsQuery, Response<ApartmentsGetFlatPaymentsForApartmentsResponseDto>>
        {
            private readonly ILogger<ApartmentsGetFlatPaymentsForApartmentsDetailsHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetFlatPaymentsForApartmentsDetailsHandler(ILogger<ApartmentsGetFlatPaymentsForApartmentsDetailsHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApartmentsGetFlatPaymentsForApartmentsResponseDto>> Handle(ApartmentsGetFlatPaymentsForApartmentsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentsGetFlatPaymentsForApartmentsDetailsAsync(request.ApartmentsId, request.FlatPaymentsId);
                return returnValue.ToResponse();
            }
        }
    }
}