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
    public class FlatsGetFlatsCountByApartmentQuery : IRequest<Response<long>>
    {
        public int FlatsApartmentId { get; set; }
        public bool FlatsIsActive { get; set; }

        public FlatsGetFlatsCountByApartmentQuery(int flatsApartmentId, bool flatsIsActive)
        {
            this.FlatsApartmentId = flatsApartmentId;
            this.FlatsIsActive = flatsIsActive;
        }

        public class FlatsGetFlatsCountByApartmentHandler : IRequestHandler<FlatsGetFlatsCountByApartmentQuery, Response<long>>
        {
            private readonly ILogger<FlatsGetFlatsCountByApartmentHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetFlatsCountByApartmentHandler(ILogger<FlatsGetFlatsCountByApartmentHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(FlatsGetFlatsCountByApartmentQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetFlatsCountByApartmentAsync(request.FlatsApartmentId, request.FlatsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}