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
    public class FlatsGetFlatExpenseInstallmentsForFlatsQuery : IRequest<Response<List<FlatsGetFlatExpenseInstallmentsForFlatsResponseDto>>>
    {
        public int FlatsId { get; set; }

        public FlatsGetFlatExpenseInstallmentsForFlatsQuery(int flatsId)
        {
            this.FlatsId = flatsId;
        }

        public class FlatsGetFlatExpenseInstallmentsForFlatsHandler : IRequestHandler<FlatsGetFlatExpenseInstallmentsForFlatsQuery, Response<List<FlatsGetFlatExpenseInstallmentsForFlatsResponseDto>>>
        {
            private readonly ILogger<FlatsGetFlatExpenseInstallmentsForFlatsHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetFlatExpenseInstallmentsForFlatsHandler(ILogger<FlatsGetFlatExpenseInstallmentsForFlatsHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<FlatsGetFlatExpenseInstallmentsForFlatsResponseDto>>> Handle(FlatsGetFlatExpenseInstallmentsForFlatsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetFlatExpenseInstallmentsForFlatsAsync(request.FlatsId);
                return returnValue.ToResponse();
            }
        }
    }
}