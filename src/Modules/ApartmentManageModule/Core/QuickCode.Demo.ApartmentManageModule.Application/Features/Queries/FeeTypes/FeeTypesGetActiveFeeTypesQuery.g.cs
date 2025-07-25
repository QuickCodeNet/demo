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
    public class FeeTypesGetActiveFeeTypesQuery : IRequest<Response<List<FeeTypesGetActiveFeeTypesResponseDto>>>
    {
        public bool FeeTypesIsActive { get; set; }

        public FeeTypesGetActiveFeeTypesQuery(bool feeTypesIsActive)
        {
            this.FeeTypesIsActive = feeTypesIsActive;
        }

        public class FeeTypesGetActiveFeeTypesHandler : IRequestHandler<FeeTypesGetActiveFeeTypesQuery, Response<List<FeeTypesGetActiveFeeTypesResponseDto>>>
        {
            private readonly ILogger<FeeTypesGetActiveFeeTypesHandler> _logger;
            private readonly IFeeTypesRepository _repository;
            public FeeTypesGetActiveFeeTypesHandler(ILogger<FeeTypesGetActiveFeeTypesHandler> logger, IFeeTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FeeTypesGetActiveFeeTypesResponseDto>>> Handle(FeeTypesGetActiveFeeTypesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FeeTypesGetActiveFeeTypesAsync(request.FeeTypesIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}