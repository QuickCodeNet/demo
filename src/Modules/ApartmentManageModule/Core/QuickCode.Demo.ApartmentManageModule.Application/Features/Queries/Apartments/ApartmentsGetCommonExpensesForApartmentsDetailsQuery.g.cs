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
    public class ApartmentsGetCommonExpensesForApartmentsDetailsQuery : IRequest<Response<ApartmentsGetCommonExpensesForApartmentsResponseDto>>
    {
        public int ApartmentsId { get; set; }
        public int CommonExpensesId { get; set; }

        public ApartmentsGetCommonExpensesForApartmentsDetailsQuery(int apartmentsId, int commonExpensesId)
        {
            this.ApartmentsId = apartmentsId;
            this.CommonExpensesId = commonExpensesId;
        }

        public class ApartmentsGetCommonExpensesForApartmentsDetailsHandler : IRequestHandler<ApartmentsGetCommonExpensesForApartmentsDetailsQuery, Response<ApartmentsGetCommonExpensesForApartmentsResponseDto>>
        {
            private readonly ILogger<ApartmentsGetCommonExpensesForApartmentsDetailsHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetCommonExpensesForApartmentsDetailsHandler(ILogger<ApartmentsGetCommonExpensesForApartmentsDetailsHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApartmentsGetCommonExpensesForApartmentsResponseDto>> Handle(ApartmentsGetCommonExpensesForApartmentsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentsGetCommonExpensesForApartmentsDetailsAsync(request.ApartmentsId, request.CommonExpensesId);
                return returnValue.ToResponse();
            }
        }
    }
}