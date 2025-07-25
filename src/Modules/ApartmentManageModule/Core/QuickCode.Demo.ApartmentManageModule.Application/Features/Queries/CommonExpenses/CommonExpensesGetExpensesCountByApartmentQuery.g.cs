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
    public class CommonExpensesGetExpensesCountByApartmentQuery : IRequest<Response<long>>
    {
        public int CommonExpensesApartmentId { get; set; }

        public CommonExpensesGetExpensesCountByApartmentQuery(int commonExpensesApartmentId)
        {
            this.CommonExpensesApartmentId = commonExpensesApartmentId;
        }

        public class CommonExpensesGetExpensesCountByApartmentHandler : IRequestHandler<CommonExpensesGetExpensesCountByApartmentQuery, Response<long>>
        {
            private readonly ILogger<CommonExpensesGetExpensesCountByApartmentHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesGetExpensesCountByApartmentHandler(ILogger<CommonExpensesGetExpensesCountByApartmentHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(CommonExpensesGetExpensesCountByApartmentQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CommonExpensesGetExpensesCountByApartmentAsync(request.CommonExpensesApartmentId);
                return returnValue.ToResponse();
            }
        }
    }
}