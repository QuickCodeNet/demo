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
    public class PaymentTypesGetItemQuery : IRequest<Response<PaymentTypesDto>>
    {
        public int Id { get; set; }

        public PaymentTypesGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class PaymentTypesGetItemHandler : IRequestHandler<PaymentTypesGetItemQuery, Response<PaymentTypesDto>>
        {
            private readonly ILogger<PaymentTypesGetItemHandler> _logger;
            private readonly IPaymentTypesRepository _repository;
            public PaymentTypesGetItemHandler(ILogger<PaymentTypesGetItemHandler> logger, IPaymentTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PaymentTypesDto>> Handle(PaymentTypesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}