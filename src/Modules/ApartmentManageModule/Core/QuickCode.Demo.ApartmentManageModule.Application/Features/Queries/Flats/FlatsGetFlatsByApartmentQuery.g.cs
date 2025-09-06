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
    public class FlatsGetFlatsByApartmentQuery : IRequest<Response<List<FlatsGetFlatsByApartmentResponseDto>>>
    {
        public int FlatsApartmentId { get; set; }
        public bool FlatsIsActive { get; set; }

        public FlatsGetFlatsByApartmentQuery(int flatsApartmentId, bool flatsIsActive)
        {
            this.FlatsApartmentId = flatsApartmentId;
            this.FlatsIsActive = flatsIsActive;
        }

        public class FlatsGetFlatsByApartmentHandler : IRequestHandler<FlatsGetFlatsByApartmentQuery, Response<List<FlatsGetFlatsByApartmentResponseDto>>>
        {
            private readonly ILogger<FlatsGetFlatsByApartmentHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetFlatsByApartmentHandler(ILogger<FlatsGetFlatsByApartmentHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatsGetFlatsByApartmentResponseDto>>> Handle(FlatsGetFlatsByApartmentQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetFlatsByApartmentAsync(request.FlatsApartmentId, request.FlatsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}