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
    public class CommonExpensesGetUnpaidExpensesByApartmentQuery : IRequest<Response<List<CommonExpensesGetUnpaidExpensesByApartmentResponseDto>>>
    {
        public int CommonExpensesApartmentId { get; set; }
        public bool CommonExpensesPaid { get; set; }

        public CommonExpensesGetUnpaidExpensesByApartmentQuery(int commonExpensesApartmentId, bool commonExpensesPaid)
        {
            this.CommonExpensesApartmentId = commonExpensesApartmentId;
            this.CommonExpensesPaid = commonExpensesPaid;
        }

        public class CommonExpensesGetUnpaidExpensesByApartmentHandler : IRequestHandler<CommonExpensesGetUnpaidExpensesByApartmentQuery, Response<List<CommonExpensesGetUnpaidExpensesByApartmentResponseDto>>>
        {
            private readonly ILogger<CommonExpensesGetUnpaidExpensesByApartmentHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesGetUnpaidExpensesByApartmentHandler(ILogger<CommonExpensesGetUnpaidExpensesByApartmentHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<CommonExpensesGetUnpaidExpensesByApartmentResponseDto>>> Handle(CommonExpensesGetUnpaidExpensesByApartmentQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CommonExpensesGetUnpaidExpensesByApartmentAsync(request.CommonExpensesApartmentId, request.CommonExpensesPaid);
                return returnValue.ToResponse();
            }
        }
    }
}