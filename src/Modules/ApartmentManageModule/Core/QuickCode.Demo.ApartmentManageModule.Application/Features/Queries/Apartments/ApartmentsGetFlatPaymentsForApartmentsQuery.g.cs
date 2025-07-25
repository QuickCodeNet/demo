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
    public class ApartmentsGetFlatPaymentsForApartmentsQuery : IRequest<Response<List<ApartmentsGetFlatPaymentsForApartmentsResponseDto>>>
    {
        public int ApartmentsId { get; set; }

        public ApartmentsGetFlatPaymentsForApartmentsQuery(int apartmentsId)
        {
            this.ApartmentsId = apartmentsId;
        }

        public class ApartmentsGetFlatPaymentsForApartmentsHandler : IRequestHandler<ApartmentsGetFlatPaymentsForApartmentsQuery, Response<List<ApartmentsGetFlatPaymentsForApartmentsResponseDto>>>
        {
            private readonly ILogger<ApartmentsGetFlatPaymentsForApartmentsHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetFlatPaymentsForApartmentsHandler(ILogger<ApartmentsGetFlatPaymentsForApartmentsHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApartmentsGetFlatPaymentsForApartmentsResponseDto>>> Handle(ApartmentsGetFlatPaymentsForApartmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentsGetFlatPaymentsForApartmentsAsync(request.ApartmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}