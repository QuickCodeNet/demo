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
    public class CommonExpensesGetTotalExpenseAmountByApartmentQuery : IRequest<Response<CommonExpensesGetTotalExpenseAmountByApartmentResponseDto>>
    {
        public int CommonExpensesApartmentId { get; set; }

        public CommonExpensesGetTotalExpenseAmountByApartmentQuery(int commonExpensesApartmentId)
        {
            this.CommonExpensesApartmentId = commonExpensesApartmentId;
        }

        public class CommonExpensesGetTotalExpenseAmountByApartmentHandler : IRequestHandler<CommonExpensesGetTotalExpenseAmountByApartmentQuery, Response<CommonExpensesGetTotalExpenseAmountByApartmentResponseDto>>
        {
            private readonly ILogger<CommonExpensesGetTotalExpenseAmountByApartmentHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesGetTotalExpenseAmountByApartmentHandler(ILogger<CommonExpensesGetTotalExpenseAmountByApartmentHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CommonExpensesGetTotalExpenseAmountByApartmentResponseDto>> Handle(CommonExpensesGetTotalExpenseAmountByApartmentQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CommonExpensesGetTotalExpenseAmountByApartmentAsync(request.CommonExpensesApartmentId);
                return returnValue.ToResponse();
            }
        }
    }
}