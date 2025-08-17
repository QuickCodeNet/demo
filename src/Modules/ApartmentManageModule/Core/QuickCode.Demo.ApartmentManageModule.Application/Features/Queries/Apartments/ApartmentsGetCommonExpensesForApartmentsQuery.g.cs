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
    public class ApartmentsGetCommonExpensesForApartmentsQuery : IRequest<Response<List<ApartmentsGetCommonExpensesForApartmentsResponseDto>>>
    {
        public int ApartmentsId { get; set; }

        public ApartmentsGetCommonExpensesForApartmentsQuery(int apartmentsId)
        {
            this.ApartmentsId = apartmentsId;
        }

        public class ApartmentsGetCommonExpensesForApartmentsHandler : IRequestHandler<ApartmentsGetCommonExpensesForApartmentsQuery, Response<List<ApartmentsGetCommonExpensesForApartmentsResponseDto>>>
        {
            private readonly ILogger<ApartmentsGetCommonExpensesForApartmentsHandler> _logger;
            private readonly IApartmentsRepository _repository;
            public ApartmentsGetCommonExpensesForApartmentsHandler(ILogger<ApartmentsGetCommonExpensesForApartmentsHandler> logger, IApartmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApartmentsGetCommonExpensesForApartmentsResponseDto>>> Handle(ApartmentsGetCommonExpensesForApartmentsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApartmentsGetCommonExpensesForApartmentsAsync(request.ApartmentsId);
                return returnValue.ToResponse();
            }
        }
    }
}