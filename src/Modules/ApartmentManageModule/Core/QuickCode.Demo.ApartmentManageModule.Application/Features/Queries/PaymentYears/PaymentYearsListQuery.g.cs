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
    public class PaymentYearsListQuery : IRequest<Response<List<PaymentYearsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public PaymentYearsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class PaymentYearsListHandler : IRequestHandler<PaymentYearsListQuery, Response<List<PaymentYearsDto>>>
        {
            private readonly ILogger<PaymentYearsListHandler> _logger;
            private readonly IPaymentYearsRepository _repository;
            public PaymentYearsListHandler(ILogger<PaymentYearsListHandler> logger, IPaymentYearsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PaymentYearsDto>>> Handle(PaymentYearsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}