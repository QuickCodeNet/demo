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
    public class FlatsGetOwnedFlatsQuery : IRequest<Response<List<FlatsGetOwnedFlatsResponseDto>>>
    {
        public bool FlatsIsActive { get; set; }

        public FlatsGetOwnedFlatsQuery(bool flatsIsActive)
        {
            this.FlatsIsActive = flatsIsActive;
        }

        public class FlatsGetOwnedFlatsHandler : IRequestHandler<FlatsGetOwnedFlatsQuery, Response<List<FlatsGetOwnedFlatsResponseDto>>>
        {
            private readonly ILogger<FlatsGetOwnedFlatsHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetOwnedFlatsHandler(ILogger<FlatsGetOwnedFlatsHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatsGetOwnedFlatsResponseDto>>> Handle(FlatsGetOwnedFlatsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetOwnedFlatsAsync(request.FlatsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}