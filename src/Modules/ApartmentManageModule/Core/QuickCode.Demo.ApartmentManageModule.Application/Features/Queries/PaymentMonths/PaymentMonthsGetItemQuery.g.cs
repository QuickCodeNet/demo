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
    public class PaymentMonthsGetItemQuery : IRequest<Response<PaymentMonthsDto>>
    {
        public int Id { get; set; }

        public PaymentMonthsGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class PaymentMonthsGetItemHandler : IRequestHandler<PaymentMonthsGetItemQuery, Response<PaymentMonthsDto>>
        {
            private readonly ILogger<PaymentMonthsGetItemHandler> _logger;
            private readonly IPaymentMonthsRepository _repository;
            public PaymentMonthsGetItemHandler(ILogger<PaymentMonthsGetItemHandler> logger, IPaymentMonthsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentMonthsDto>> Handle(PaymentMonthsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}