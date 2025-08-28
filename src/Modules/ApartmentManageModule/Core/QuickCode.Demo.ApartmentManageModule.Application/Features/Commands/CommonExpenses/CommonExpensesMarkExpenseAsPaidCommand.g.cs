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
    public class CommonExpensesMarkExpenseAsPaidCommand : IRequest<Response<int>>
    {
        public int CommonExpensesId { get; set; }
        public CommonExpensesMarkExpenseAsPaidRequestDto UpdateRequest { get; set; }

        public CommonExpensesMarkExpenseAsPaidCommand(int commonExpensesId, CommonExpensesMarkExpenseAsPaidRequestDto updateRequest)
        {
            this.CommonExpensesId = commonExpensesId;
            this.UpdateRequest = updateRequest;
        }

        public class CommonExpensesMarkExpenseAsPaidHandler : IRequestHandler<CommonExpensesMarkExpenseAsPaidCommand, Response<int>>
        {
            private readonly ILogger<CommonExpensesMarkExpenseAsPaidHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesMarkExpenseAsPaidHandler(ILogger<CommonExpensesMarkExpenseAsPaidHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(CommonExpensesMarkExpenseAsPaidCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CommonExpensesMarkExpenseAsPaidAsync(request.CommonExpensesId, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}