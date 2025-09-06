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
    public class FeeTypesGetFlatPaymentsForFeeTypesQuery : IRequest<Response<List<FeeTypesGetFlatPaymentsForFeeTypesResponseDto>>>
    {
        public int FeeTypesId { get; set; }

        public FeeTypesGetFlatPaymentsForFeeTypesQuery(int feeTypesId)
        {
            this.FeeTypesId = feeTypesId;
        }

        public class FeeTypesGetFlatPaymentsForFeeTypesHandler : IRequestHandler<FeeTypesGetFlatPaymentsForFeeTypesQuery, Response<List<FeeTypesGetFlatPaymentsForFeeTypesResponseDto>>>
        {
            private readonly ILogger<FeeTypesGetFlatPaymentsForFeeTypesHandler> _logger;
            private readonly IFeeTypesRepository _repository;
            public FeeTypesGetFlatPaymentsForFeeTypesHandler(ILogger<FeeTypesGetFlatPaymentsForFeeTypesHandler> logger, IFeeTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FeeTypesGetFlatPaymentsForFeeTypesResponseDto>>> Handle(FeeTypesGetFlatPaymentsForFeeTypesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FeeTypesGetFlatPaymentsForFeeTypesAsync(request.FeeTypesId);
                return returnValue.ToResponse();
            }
        }
    }
}