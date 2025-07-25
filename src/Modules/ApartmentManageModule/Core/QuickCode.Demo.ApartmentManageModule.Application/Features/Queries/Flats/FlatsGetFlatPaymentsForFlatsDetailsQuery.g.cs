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
    public class FlatsGetFlatPaymentsForFlatsDetailsQuery : IRequest<Response<FlatsGetFlatPaymentsForFlatsResponseDto>>
    {
        public int FlatsId { get; set; }
        public int FlatPaymentsId { get; set; }

        public FlatsGetFlatPaymentsForFlatsDetailsQuery(int flatsId, int flatPaymentsId)
        {
            this.FlatsId = flatsId;
            this.FlatPaymentsId = flatPaymentsId;
        }

        public class FlatsGetFlatPaymentsForFlatsDetailsHandler : IRequestHandler<FlatsGetFlatPaymentsForFlatsDetailsQuery, Response<FlatsGetFlatPaymentsForFlatsResponseDto>>
        {
            private readonly ILogger<FlatsGetFlatPaymentsForFlatsDetailsHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetFlatPaymentsForFlatsDetailsHandler(ILogger<FlatsGetFlatPaymentsForFlatsDetailsHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FlatsGetFlatPaymentsForFlatsResponseDto>> Handle(FlatsGetFlatPaymentsForFlatsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetFlatPaymentsForFlatsDetailsAsync(request.FlatsId, request.FlatPaymentsId);
                return returnValue.ToResponse();
            }
        }
    }
}