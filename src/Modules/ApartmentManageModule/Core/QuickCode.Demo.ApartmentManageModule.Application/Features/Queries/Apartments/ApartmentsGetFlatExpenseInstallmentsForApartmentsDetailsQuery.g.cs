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
    public class ApartmentsGetFlatExpenseInstallmentsForApartmentsDetailsQuery : IRequest<Response<ApartmentsGetFlatExpenseInstallmentsForApartmentsResponseDto>>
    {
        public int ApartmentsId { get; set; }
        public int FlatExpenseInstallmentsId { get; set; }

        public ApartmentsGetFlatExpenseInstallmentsForApartmentsDetailsQuery(int apartmentsId, int flatExpenseInstallmentsId)
        {
            this.ApartmentsId = apartmentsId;
            this.FlatExpenseInstallmentsId = flatExpenseInstallmentsId;
        }

        public class ApartmentsGetFlatExpenseInstallmentsForApartmentsDetailsHandler : IRequestHandler<ApartmentsGetFlatExpenseInstallmentsForApartmentsDetailsQuery, Response<ApartmentsGetFlatExpenseInstallmentsForApartmentsResponseDto>>
        {
            private readonly ILogger<ApartmentsGetFlatExpenseInstallmentsForApartmentsDetailsHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetFlatExpenseInstallmentsForApartmentsDetailsHandler(ILogger<ApartmentsGetFlatExpenseInstallmentsForApartmentsDetailsHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApartmentsGetFlatExpenseInstallmentsForApartmentsResponseDto>> Handle(ApartmentsGetFlatExpenseInstallmentsForApartmentsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentsGetFlatExpenseInstallmentsForApartmentsDetailsAsync(request.ApartmentsId, request.FlatExpenseInstallmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}