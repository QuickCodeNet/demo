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
    public class ApartmentsGetFlatsForApartmentsQuery : IRequest<Response<List<ApartmentsGetFlatsForApartmentsResponseDto>>>
    {
        public int ApartmentsId { get; set; }

        public ApartmentsGetFlatsForApartmentsQuery(int apartmentsId)
        {
            this.ApartmentsId = apartmentsId;
        }

        public class ApartmentsGetFlatsForApartmentsHandler : IRequestHandler<ApartmentsGetFlatsForApartmentsQuery, Response<List<ApartmentsGetFlatsForApartmentsResponseDto>>>
        {
            private readonly ILogger<ApartmentsGetFlatsForApartmentsHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetFlatsForApartmentsHandler(ILogger<ApartmentsGetFlatsForApartmentsHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApartmentsGetFlatsForApartmentsResponseDto>>> Handle(ApartmentsGetFlatsForApartmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentsGetFlatsForApartmentsAsync(request.ApartmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}