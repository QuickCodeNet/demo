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
    public class FlatsGetRentedFlatsQuery : IRequest<Response<List<FlatsGetRentedFlatsResponseDto>>>
    {
        public bool FlatsIsActive { get; set; }

        public FlatsGetRentedFlatsQuery(bool flatsIsActive)
        {
            this.FlatsIsActive = flatsIsActive;
        }

        public class FlatsGetRentedFlatsHandler : IRequestHandler<FlatsGetRentedFlatsQuery, Response<List<FlatsGetRentedFlatsResponseDto>>>
        {
            private readonly ILogger<FlatsGetRentedFlatsHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetRentedFlatsHandler(ILogger<FlatsGetRentedFlatsHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatsGetRentedFlatsResponseDto>>> Handle(FlatsGetRentedFlatsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetRentedFlatsAsync(request.FlatsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}