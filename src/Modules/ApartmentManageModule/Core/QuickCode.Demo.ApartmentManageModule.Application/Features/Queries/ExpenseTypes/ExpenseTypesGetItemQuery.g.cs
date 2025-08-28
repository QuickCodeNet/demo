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
    public class ExpenseTypesGetItemQuery : IRequest<Response<ExpenseTypesDto>>
    {
        public int Id { get; set; }

        public ExpenseTypesGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class ExpenseTypesGetItemHandler : IRequestHandler<ExpenseTypesGetItemQuery, Response<ExpenseTypesDto>>
        {
            private readonly ILogger<ExpenseTypesGetItemHandler> _logger;
            private readonly IExpenseTypesRepository _repository;
            public ExpenseTypesGetItemHandler(ILogger<ExpenseTypesGetItemHandler> logger, IExpenseTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ExpenseTypesDto>> Handle(ExpenseTypesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}