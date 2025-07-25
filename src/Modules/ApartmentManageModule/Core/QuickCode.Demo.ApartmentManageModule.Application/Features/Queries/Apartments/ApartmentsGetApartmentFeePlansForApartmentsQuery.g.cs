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
    public class ApartmentsGetApartmentFeePlansForApartmentsQuery : IRequest<Response<List<ApartmentsGetApartmentFeePlansForApartmentsResponseDto>>>
    {
        public int ApartmentsId { get; set; }

        public ApartmentsGetApartmentFeePlansForApartmentsQuery(int apartmentsId)
        {
            this.ApartmentsId = apartmentsId;
        }

        public class ApartmentsGetApartmentFeePlansForApartmentsHandler : IRequestHandler<ApartmentsGetApartmentFeePlansForApartmentsQuery, Response<List<ApartmentsGetApartmentFeePlansForApartmentsResponseDto>>>
        {
            private readonly ILogger<ApartmentsGetApartmentFeePlansForApartmentsHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetApartmentFeePlansForApartmentsHandler(ILogger<ApartmentsGetApartmentFeePlansForApartmentsHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApartmentsGetApartmentFeePlansForApartmentsResponseDto>>> Handle(ApartmentsGetApartmentFeePlansForApartmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentsGetApartmentFeePlansForApartmentsAsync(request.ApartmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}