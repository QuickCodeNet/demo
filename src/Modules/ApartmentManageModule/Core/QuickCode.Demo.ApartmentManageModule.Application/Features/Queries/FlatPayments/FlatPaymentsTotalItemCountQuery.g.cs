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
    public class FlatPaymentsTotalItemCountQuery : IRequest<Response<int>>
    {
        public FlatPaymentsTotalItemCountQuery()
        {
        }

        public class FlatPaymentsTotalItemCountHandler : IRequestHandler<FlatPaymentsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<FlatPaymentsTotalItemCountHandler> _logger;
            private readonly IFlatPaymentsRepository _repository;
            public FlatPaymentsTotalItemCountHandler(ILogger<FlatPaymentsTotalItemCountHandler> logger, IFlatPaymentsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(FlatPaymentsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}