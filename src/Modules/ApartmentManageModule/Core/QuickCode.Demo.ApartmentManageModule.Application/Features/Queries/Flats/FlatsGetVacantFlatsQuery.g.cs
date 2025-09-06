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
    public class FlatsGetVacantFlatsQuery : IRequest<Response<List<FlatsGetVacantFlatsResponseDto>>>
    {
        public bool FlatsIsActive { get; set; }

        public FlatsGetVacantFlatsQuery(bool flatsIsActive)
        {
            this.FlatsIsActive = flatsIsActive;
        }

        public class FlatsGetVacantFlatsHandler : IRequestHandler<FlatsGetVacantFlatsQuery, Response<List<FlatsGetVacantFlatsResponseDto>>>
        {
            private readonly ILogger<FlatsGetVacantFlatsHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetVacantFlatsHandler(ILogger<FlatsGetVacantFlatsHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatsGetVacantFlatsResponseDto>>> Handle(FlatsGetVacantFlatsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetVacantFlatsAsync(request.FlatsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}