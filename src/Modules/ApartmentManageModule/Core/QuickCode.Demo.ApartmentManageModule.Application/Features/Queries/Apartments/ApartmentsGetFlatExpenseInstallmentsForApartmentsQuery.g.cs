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
    public class ApartmentsGetFlatExpenseInstallmentsForApartmentsQuery : IRequest<Response<List<ApartmentsGetFlatExpenseInstallmentsForApartmentsResponseDto>>>
    {
        public int ApartmentsId { get; set; }

        public ApartmentsGetFlatExpenseInstallmentsForApartmentsQuery(int apartmentsId)
        {
            this.ApartmentsId = apartmentsId;
        }

        public class ApartmentsGetFlatExpenseInstallmentsForApartmentsHandler : IRequestHandler<ApartmentsGetFlatExpenseInstallmentsForApartmentsQuery, Response<List<ApartmentsGetFlatExpenseInstallmentsForApartmentsResponseDto>>>
        {
            private readonly ILogger<ApartmentsGetFlatExpenseInstallmentsForApartmentsHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetFlatExpenseInstallmentsForApartmentsHandler(ILogger<ApartmentsGetFlatExpenseInstallmentsForApartmentsHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApartmentsGetFlatExpenseInstallmentsForApartmentsResponseDto>>> Handle(ApartmentsGetFlatExpenseInstallmentsForApartmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentsGetFlatExpenseInstallmentsForApartmentsAsync(request.ApartmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}