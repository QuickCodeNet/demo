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
    public class CommonExpensesGetFlatPaymentsForCommonExpensesQuery : IRequest<Response<List<CommonExpensesGetFlatPaymentsForCommonExpensesResponseDto>>>
    {
        public int CommonExpensesId { get; set; }

        public CommonExpensesGetFlatPaymentsForCommonExpensesQuery(int commonExpensesId)
        {
            this.CommonExpensesId = commonExpensesId;
        }

        public class CommonExpensesGetFlatPaymentsForCommonExpensesHandler : IRequestHandler<CommonExpensesGetFlatPaymentsForCommonExpensesQuery, Response<List<CommonExpensesGetFlatPaymentsForCommonExpensesResponseDto>>>
        {
            private readonly ILogger<CommonExpensesGetFlatPaymentsForCommonExpensesHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesGetFlatPaymentsForCommonExpensesHandler(ILogger<CommonExpensesGetFlatPaymentsForCommonExpensesHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<CommonExpensesGetFlatPaymentsForCommonExpensesResponseDto>>> Handle(CommonExpensesGetFlatPaymentsForCommonExpensesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CommonExpensesGetFlatPaymentsForCommonExpensesAsync(request.CommonExpensesId);
                return returnValue.ToResponse();
            }
        }
    }
}