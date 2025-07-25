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
    public class ApartmentsGetActiveApartmentsQuery : IRequest<Response<List<ApartmentsGetActiveApartmentsResponseDto>>>
    {
        public bool ApartmentsIsActive { get; set; }

        public ApartmentsGetActiveApartmentsQuery(bool apartmentsIsActive)
        {
            this.ApartmentsIsActive = apartmentsIsActive;
        }

        public class ApartmentsGetActiveApartmentsHandler : IRequestHandler<ApartmentsGetActiveApartmentsQuery, Response<List<ApartmentsGetActiveApartmentsResponseDto>>>
        {
            private readonly ILogger<ApartmentsGetActiveApartmentsHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetActiveApartmentsHandler(ILogger<ApartmentsGetActiveApartmentsHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApartmentsGetActiveApartmentsResponseDto>>> Handle(ApartmentsGetActiveApartmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentsGetActiveApartmentsAsync(request.ApartmentsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}