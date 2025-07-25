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
    public class ExpenseInstallmentsUpdateCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public ExpenseInstallmentsDto request { get; set; }

        public ExpenseInstallmentsUpdateCommand(int id, ExpenseInstallmentsDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class ExpenseInstallmentsUpdateHandler : IRequestHandler<ExpenseInstallmentsUpdateCommand, Response<bool>>
        {
            private readonly ILogger<ExpenseInstallmentsUpdateHandler> _logger;
            private readonly IExpenseInstallmentsRepository _repository;
            public ExpenseInstallmentsUpdateHandler(ILogger<ExpenseInstallmentsUpdateHandler> logger, IExpenseInstallmentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ExpenseInstallmentsUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}