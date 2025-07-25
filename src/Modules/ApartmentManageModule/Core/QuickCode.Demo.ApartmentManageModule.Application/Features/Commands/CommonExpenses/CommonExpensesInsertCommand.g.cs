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
    public class CommonExpensesInsertCommand : IRequest<Response<CommonExpensesDto>>
    {
        public CommonExpensesDto request { get; set; }

        public CommonExpensesInsertCommand(CommonExpensesDto request)
        {
            this.request = request;
        }

        public class CommonExpensesInsertHandler : IRequestHandler<CommonExpensesInsertCommand, Response<CommonExpensesDto>>
        {
            private readonly ILogger<CommonExpensesInsertHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesInsertHandler(ILogger<CommonExpensesInsertHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CommonExpensesDto>> Handle(CommonExpensesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}