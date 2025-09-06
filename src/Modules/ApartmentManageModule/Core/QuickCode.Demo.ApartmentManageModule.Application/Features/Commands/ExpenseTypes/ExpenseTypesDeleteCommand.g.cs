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
    public class ExpenseTypesDeleteCommand : IRequest<Response<bool>>
    {
        public ExpenseTypesDto request { get; set; }

        public ExpenseTypesDeleteCommand(ExpenseTypesDto request)
        {
            this.request = request;
        }

        public class ExpenseTypesDeleteHandler : IRequestHandler<ExpenseTypesDeleteCommand, Response<bool>>
        {
            private readonly ILogger<ExpenseTypesDeleteHandler> _logger;
            private readonly IExpenseTypesRepository _repository;
            public ExpenseTypesDeleteHandler(ILogger<ExpenseTypesDeleteHandler> logger, IExpenseTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ExpenseTypesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}