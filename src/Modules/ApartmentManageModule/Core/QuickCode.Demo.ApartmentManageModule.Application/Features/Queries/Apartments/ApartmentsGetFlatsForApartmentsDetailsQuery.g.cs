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
    public class ApartmentsGetFlatsForApartmentsDetailsQuery : IRequest<Response<ApartmentsGetFlatsForApartmentsResponseDto>>
    {
        public int ApartmentsId { get; set; }
        public int FlatsId { get; set; }

        public ApartmentsGetFlatsForApartmentsDetailsQuery(int apartmentsId, int flatsId)
        {
            this.ApartmentsId = apartmentsId;
            this.FlatsId = flatsId;
        }

        public class ApartmentsGetFlatsForApartmentsDetailsHandler : IRequestHandler<ApartmentsGetFlatsForApartmentsDetailsQuery, Response<ApartmentsGetFlatsForApartmentsResponseDto>>
        {
            private readonly ILogger<ApartmentsGetFlatsForApartmentsDetailsHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetFlatsForApartmentsDetailsHandler(ILogger<ApartmentsGetFlatsForApartmentsDetailsHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApartmentsGetFlatsForApartmentsResponseDto>> Handle(ApartmentsGetFlatsForApartmentsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentsGetFlatsForApartmentsDetailsAsync(request.ApartmentsId, request.FlatsId);
                return returnValue.ToResponse();
            }
        }
    }
}