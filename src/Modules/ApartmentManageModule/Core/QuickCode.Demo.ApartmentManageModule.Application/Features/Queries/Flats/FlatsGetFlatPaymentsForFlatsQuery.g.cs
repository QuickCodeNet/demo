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
    public class FlatsGetFlatPaymentsForFlatsQuery : IRequest<Response<List<FlatsGetFlatPaymentsForFlatsResponseDto>>>
    {
        public int FlatsId { get; set; }

        public FlatsGetFlatPaymentsForFlatsQuery(int flatsId)
        {
            this.FlatsId = flatsId;
        }

        public class FlatsGetFlatPaymentsForFlatsHandler : IRequestHandler<FlatsGetFlatPaymentsForFlatsQuery, Response<List<FlatsGetFlatPaymentsForFlatsResponseDto>>>
        {
            private readonly ILogger<FlatsGetFlatPaymentsForFlatsHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetFlatPaymentsForFlatsHandler(ILogger<FlatsGetFlatPaymentsForFlatsHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatsGetFlatPaymentsForFlatsResponseDto>>> Handle(FlatsGetFlatPaymentsForFlatsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetFlatPaymentsForFlatsAsync(request.FlatsId);
                return returnValue.ToResponse();
            }
        }
    }
}