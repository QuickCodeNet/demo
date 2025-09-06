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
    public class ApartmentsGetExpenseInstallmentsForApartmentsQuery : IRequest<Response<List<ApartmentsGetExpenseInstallmentsForApartmentsResponseDto>>>
    {
        public int ApartmentsId { get; set; }

        public ApartmentsGetExpenseInstallmentsForApartmentsQuery(int apartmentsId)
        {
            this.ApartmentsId = apartmentsId;
        }

        public class ApartmentsGetExpenseInstallmentsForApartmentsHandler : IRequestHandler<ApartmentsGetExpenseInstallmentsForApartmentsQuery, Response<List<ApartmentsGetExpenseInstallmentsForApartmentsResponseDto>>>
        {
            private readonly ILogger<ApartmentsGetExpenseInstallmentsForApartmentsHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetExpenseInstallmentsForApartmentsHandler(ILogger<ApartmentsGetExpenseInstallmentsForApartmentsHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApartmentsGetExpenseInstallmentsForApartmentsResponseDto>>> Handle(ApartmentsGetExpenseInstallmentsForApartmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentsGetExpenseInstallmentsForApartmentsAsync(request.ApartmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}