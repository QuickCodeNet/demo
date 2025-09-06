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
    public class CommonExpensesGetFlatPaymentsForCommonExpensesDetailsQuery : IRequest<Response<CommonExpensesGetFlatPaymentsForCommonExpensesResponseDto>>
    {
        public int CommonExpensesId { get; set; }
        public int FlatPaymentsId { get; set; }

        public CommonExpensesGetFlatPaymentsForCommonExpensesDetailsQuery(int commonExpensesId, int flatPaymentsId)
        {
            this.CommonExpensesId = commonExpensesId;
            this.FlatPaymentsId = flatPaymentsId;
        }

        public class CommonExpensesGetFlatPaymentsForCommonExpensesDetailsHandler : IRequestHandler<CommonExpensesGetFlatPaymentsForCommonExpensesDetailsQuery, Response<CommonExpensesGetFlatPaymentsForCommonExpensesResponseDto>>
        {
            private readonly ILogger<CommonExpensesGetFlatPaymentsForCommonExpensesDetailsHandler> _logger;
            private readonly ICommonExpensesRepository _repository;
            public CommonExpensesGetFlatPaymentsForCommonExpensesDetailsHandler(ILogger<CommonExpensesGetFlatPaymentsForCommonExpensesDetailsHandler> logger, ICommonExpensesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<CommonExpensesGetFlatPaymentsForCommonExpensesResponseDto>> Handle(CommonExpensesGetFlatPaymentsForCommonExpensesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CommonExpensesGetFlatPaymentsForCommonExpensesDetailsAsync(request.CommonExpensesId, request.FlatPaymentsId);
                return returnValue.ToResponse();
            }
        }
    }
}