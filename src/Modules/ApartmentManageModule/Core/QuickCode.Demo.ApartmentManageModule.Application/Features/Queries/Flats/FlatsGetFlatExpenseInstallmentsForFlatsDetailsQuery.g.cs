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
    public class FlatsGetFlatExpenseInstallmentsForFlatsDetailsQuery : IRequest<Response<FlatsGetFlatExpenseInstallmentsForFlatsResponseDto>>
    {
        public int FlatsId { get; set; }
        public int FlatExpenseInstallmentsId { get; set; }

        public FlatsGetFlatExpenseInstallmentsForFlatsDetailsQuery(int flatsId, int flatExpenseInstallmentsId)
        {
            this.FlatsId = flatsId;
            this.FlatExpenseInstallmentsId = flatExpenseInstallmentsId;
        }

        public class FlatsGetFlatExpenseInstallmentsForFlatsDetailsHandler : IRequestHandler<FlatsGetFlatExpenseInstallmentsForFlatsDetailsQuery, Response<FlatsGetFlatExpenseInstallmentsForFlatsResponseDto>>
        {
            private readonly ILogger<FlatsGetFlatExpenseInstallmentsForFlatsDetailsHandler> _logger;
            private readonly IFlatsRepository _repository;
            public FlatsGetFlatExpenseInstallmentsForFlatsDetailsHandler(ILogger<FlatsGetFlatExpenseInstallmentsForFlatsDetailsHandler> logger, IFlatsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FlatsGetFlatExpenseInstallmentsForFlatsResponseDto>> Handle(FlatsGetFlatExpenseInstallmentsForFlatsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.FlatsGetFlatExpenseInstallmentsForFlatsDetailsAsync(request.FlatsId, request.FlatExpenseInstallmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}