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
    public class FeeTypesGetFlatPaymentsForFeeTypesDetailsQuery : IRequest<Response<FeeTypesGetFlatPaymentsForFeeTypesResponseDto>>
    {
        public int FeeTypesId { get; set; }
        public int FlatPaymentsId { get; set; }

        public FeeTypesGetFlatPaymentsForFeeTypesDetailsQuery(int feeTypesId, int flatPaymentsId)
        {
            this.FeeTypesId = feeTypesId;
            this.FlatPaymentsId = flatPaymentsId;
        }

        public class FeeTypesGetFlatPaymentsForFeeTypesDetailsHandler : IRequestHandler<FeeTypesGetFlatPaymentsForFeeTypesDetailsQuery, Response<FeeTypesGetFlatPaymentsForFeeTypesResponseDto>>
        {
            private readonly ILogger<FeeTypesGetFlatPaymentsForFeeTypesDetailsHandler> _logger;
            private readonly IFeeTypesRepository _repository;
            public FeeTypesGetFlatPaymentsForFeeTypesDetailsHandler(ILogger<FeeTypesGetFlatPaymentsForFeeTypesDetailsHandler> logger, IFeeTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FeeTypesGetFlatPaymentsForFeeTypesResponseDto>> Handle(FeeTypesGetFlatPaymentsForFeeTypesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FeeTypesGetFlatPaymentsForFeeTypesDetailsAsync(request.FeeTypesId, request.FlatPaymentsId);
                return returnValue.ToResponse();
            }
        }
    }
}