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
    public class ExpenseTypesInsertCommand : IRequest<Response<ExpenseTypesDto>>
    {
        public ExpenseTypesDto request { get; set; }

        public ExpenseTypesInsertCommand(ExpenseTypesDto request)
        {
            this.request = request;
        }

        public class ExpenseTypesInsertHandler : IRequestHandler<ExpenseTypesInsertCommand, Response<ExpenseTypesDto>>
        {
            private readonly ILogger<ExpenseTypesInsertHandler> _logger;
            private readonly IExpenseTypesRepository _repository;
            public ExpenseTypesInsertHandler(ILogger<ExpenseTypesInsertHandler> logger, IExpenseTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ExpenseTypesDto>> Handle(ExpenseTypesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}