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
    public class PaymentMonthsListQuery : IRequest<Response<List<PaymentMonthsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public PaymentMonthsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class PaymentMonthsListHandler : IRequestHandler<PaymentMonthsListQuery, Response<List<PaymentMonthsDto>>>
        {
            private readonly ILogger<PaymentMonthsListHandler> _logger;
            private readonly IPaymentMonthsRepository _repository;
            public PaymentMonthsListHandler(ILogger<PaymentMonthsListHandler> logger, IPaymentMonthsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentMonthsDto>>> Handle(PaymentMonthsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}