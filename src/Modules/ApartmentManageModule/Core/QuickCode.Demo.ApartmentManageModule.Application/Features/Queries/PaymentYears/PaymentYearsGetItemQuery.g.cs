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
    public class PaymentYearsGetItemQuery : IRequest<Response<PaymentYearsDto>>
    {
        public int Id { get; set; }

        public PaymentYearsGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class PaymentYearsGetItemHandler : IRequestHandler<PaymentYearsGetItemQuery, Response<PaymentYearsDto>>
        {
            private readonly ILogger<PaymentYearsGetItemHandler> _logger;
            private readonly IPaymentYearsRepository _repository;
            public PaymentYearsGetItemHandler(ILogger<PaymentYearsGetItemHandler> logger, IPaymentYearsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentYearsDto>> Handle(PaymentYearsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}