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
    public class CommonExpensesGetItemQuery : IRequest<Response<CommonExpensesDto>>
    {
        public int Id { get; set; }

        public CommonExpensesGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class CommonExpensesGetItemHandler : IRequestHandler<CommonExpensesGetItemQuery, Response<CommonExpensesDto>>
        {
            private readonly ILogger<CommonExpensesGetItemHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesGetItemHandler(ILogger<CommonExpensesGetItemHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CommonExpensesDto>> Handle(CommonExpensesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}